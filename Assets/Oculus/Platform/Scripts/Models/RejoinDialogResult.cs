// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class RejoinDialogResult
    {
        /// A boolean for if the user decided to rejoin.
        public readonly bool RejoinSelected;


        public RejoinDialogResult(IntPtr o)
        {
            RejoinSelected = CAPI.ovr_RejoinDialogResult_GetRejoinSelected(o);
        }
    }
}