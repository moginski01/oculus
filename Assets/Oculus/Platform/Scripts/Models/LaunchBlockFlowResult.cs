// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class LaunchBlockFlowResult
    {
        /// Whether the viewer successfully blocked the user.
        public readonly bool DidBlock;

        /// Whether the viewer chose to cancel the block flow.
        public readonly bool DidCancel;


        public LaunchBlockFlowResult(IntPtr o)
        {
            DidBlock = CAPI.ovr_LaunchBlockFlowResult_GetDidBlock(o);
            DidCancel = CAPI.ovr_LaunchBlockFlowResult_GetDidCancel(o);
        }
    }
}