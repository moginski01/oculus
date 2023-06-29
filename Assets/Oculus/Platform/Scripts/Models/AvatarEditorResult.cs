// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class AvatarEditorResult
    {
        /// Whether the request has sent.
        public readonly bool RequestSent;


        public AvatarEditorResult(IntPtr o)
        {
            RequestSent = CAPI.ovr_AvatarEditorResult_GetRequestSent(o);
        }
    }
}