using System;
using UnityEngine;

namespace Oculus.Platform
{
    public class VoipAudioSourceHiLevel : MonoBehaviour
    {
        private static int audioSystemPlaybackFrequency;
        private static readonly bool verboseLogging = false;

        public AudioSource audioSource;
        public float peakAmplitude;


        private int initialPlaybackDelayMS;

        protected IVoipPCMSource pcmSource;

        public ulong senderID
        {
            set => pcmSource.SetSenderID(value);
        }

        protected void Awake()
        {
            CreatePCMSource();
            if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.gameObject.AddComponent<FilterReadDelegate>();
            var filterDelegate = audioSource.gameObject.GetComponent<FilterReadDelegate>();
            filterDelegate.parent = this;

            initialPlaybackDelayMS = 40;

            audioSystemPlaybackFrequency = AudioSettings.outputSampleRate;
            CAPI.ovr_Voip_SetOutputSampleRate(SampleRateToEnum(audioSystemPlaybackFrequency));
            if (verboseLogging) Debug.LogFormat("freq {0}", audioSystemPlaybackFrequency);
        }

        private void Start()
        {
            audioSource.Stop();
        }

        private void Update()
        {
            pcmSource.Update();

            if (!audioSource.isPlaying && pcmSource.PeekSizeElements() >= MSToElements(initialPlaybackDelayMS))
            {
                if (verboseLogging)
                    Debug.LogFormat("buffered {0} elements, starting playback", pcmSource.PeekSizeElements());
                audioSource.Play();
            }
        }

        protected void Stop()
        {
        }

        private VoipSampleRate SampleRateToEnum(int rate)
        {
            switch (rate)
            {
                case 48000:
                    return VoipSampleRate.HZ48000;
                case 44100:
                    return VoipSampleRate.HZ44100;
                case 24000:
                    return VoipSampleRate.HZ24000;
                default:
                    return VoipSampleRate.Unknown;
            }
        }

        protected virtual void CreatePCMSource()
        {
            pcmSource = new VoipPCMSourceNative();
        }

        protected static int MSToElements(int ms)
        {
            return ms * audioSystemPlaybackFrequency / 1000;
        }

        // This is a delegate that exists as a surface for OnAudioFilterRead
        // It will be callled on unity's audio thread
        public class FilterReadDelegate : MonoBehaviour
        {
            public VoipAudioSourceHiLevel parent;
            private float[] scratchBuffer;

            private void Awake()
            {
                var bufferSizeElements = (int)CAPI.ovr_Voip_GetOutputBufferMaxSize();
                scratchBuffer = new float[bufferSizeElements];
            }

            private void OnAudioFilterRead(float[] data, int channels)
            {
                var sizeToFetch = data.Length / channels;
                var sourceBufferSize = sizeToFetch;
                if (sourceBufferSize > scratchBuffer.Length)
                {
                    Array.Clear(data, 0, data.Length);
                    throw new Exception(string.Format(
                        "Audio system tried to pull {0} bytes, max voip internal ring buffer size {1}", sizeToFetch,
                        scratchBuffer.Length));
                }

                var available = parent.pcmSource.PeekSizeElements();
                if (available < sourceBufferSize)
                {
                    if (verboseLogging)
                        Debug.LogFormat(
                            "Voip starved! Want {0}, but only have {1} available",
                            sourceBufferSize,
                            available);
                    return;
                }

                var copied = parent.pcmSource.GetPCM(scratchBuffer, sourceBufferSize);
                if (copied < sourceBufferSize)
                {
                    Debug.LogWarningFormat(
                        "GetPCM() returned {0} samples, expected {1}",
                        copied,
                        sourceBufferSize);

                    return;
                }

                var dest = 0;
                float tmpPeakAmp = -1;
                for (var i = 0; i < sizeToFetch; i++)
                {
                    var val = scratchBuffer[i];

                    for (var j = 0; j < channels; j++)
                    {
                        data[dest++] = val;
                        if (val > tmpPeakAmp) tmpPeakAmp = val;
                    }
                }

                parent.peakAmplitude = tmpPeakAmp;
            }
        }
    }
}