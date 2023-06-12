// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class UserDataStoreUpdateResponse
    {
        /// Whether the update request is succeeded.
        public readonly bool Success;


        public UserDataStoreUpdateResponse(IntPtr o)
        {
            Success = CAPI.ovr_UserDataStoreUpdateResponse_GetSuccess(o);
        }
    }
}