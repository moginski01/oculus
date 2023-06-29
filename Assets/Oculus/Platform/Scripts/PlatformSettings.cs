using System.IO;
using UnityEditor;
using UnityEngine;

namespace Oculus.Platform
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public sealed class PlatformSettings : ScriptableObject
    {
        public static string AppID
        {
            get => Instance.ovrAppID;
            set => Instance.ovrAppID = value;
        }

        public static string MobileAppID
        {
            get => Instance.ovrMobileAppID;
            set => Instance.ovrMobileAppID = value;
        }

        public static bool UseStandalonePlatform
        {
            get => Instance.ovrUseStandalonePlatform;
            set => Instance.ovrUseStandalonePlatform = value;
        }

        [SerializeField] private string ovrAppID = "";

        [SerializeField] private string ovrMobileAppID = "";

#if UNITY_EDITOR_WIN
        [SerializeField] private bool ovrUseStandalonePlatform;
#else
    [SerializeField]
    private bool ovrUseStandalonePlatform = true;
#endif

        private static PlatformSettings instance;

        public static PlatformSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<PlatformSettings>("OculusPlatformSettings");

                    // This can happen if the developer never input their App Id into the Unity Editor
                    // and therefore never created the OculusPlatformSettings.asset file
                    // Use a dummy object with defaults for the getters so we don't have a null pointer exception
                    if (instance == null)
                    {
                        instance = CreateInstance<PlatformSettings>();

#if UNITY_EDITOR
                        // Only in the editor should we save it to disk
                        var properPath = Path.Combine(UnityEngine.Application.dataPath, "Resources");
                        if (!Directory.Exists(properPath)) AssetDatabase.CreateFolder("Assets", "Resources");

                        var fullPath = Path.Combine(
                            Path.Combine("Assets", "Resources"),
                            "OculusPlatformSettings.asset"
                        );
                        AssetDatabase.CreateAsset(instance, fullPath);
#endif
                    }
                }

                return instance;
            }

            set { instance = value; }
        }
    }
}