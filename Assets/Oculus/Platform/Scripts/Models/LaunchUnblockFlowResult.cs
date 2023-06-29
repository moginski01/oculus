// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class LaunchUnblockFlowResult
    {
        /// Whether the viewer chose to cancel the unblock flow.
        public readonly bool DidCancel;

        /// Whether the viewer successfully unblocked the user.
        public readonly bool DidUnblock;


        public LaunchUnblockFlowResult(IntPtr o)
        {
            DidCancel = CAPI.ovr_LaunchUnblockFlowResult_GetDidCancel(o);
            DidUnblock = CAPI.ovr_LaunchUnblockFlowResult_GetDidUnblock(o);
        }
    }
}