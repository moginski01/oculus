// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class LaunchFriendRequestFlowResult
    {
        /// Whether the viewer chose to cancel the friend request flow.
        public readonly bool DidCancel;

        /// Whether the viewer successfully sent the friend request.
        public readonly bool DidSendRequest;


        public LaunchFriendRequestFlowResult(IntPtr o)
        {
            DidCancel = CAPI.ovr_LaunchFriendRequestFlowResult_GetDidCancel(o);
            DidSendRequest = CAPI.ovr_LaunchFriendRequestFlowResult_GetDidSendRequest(o);
        }
    }
}