// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class LivestreamingStartResult
    {
        public readonly LivestreamingStartStatus StreamingResult;


        public LivestreamingStartResult(IntPtr o)
        {
            StreamingResult = CAPI.ovr_LivestreamingStartResult_GetStreamingResult(o);
        }
    }
}