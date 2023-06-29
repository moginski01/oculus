// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class LaunchInvitePanelFlowResult
    {
        /// A list of users that were sent an invitation to the session.
        public readonly UserList InvitedUsers;


        public LaunchInvitePanelFlowResult(IntPtr o)
        {
            InvitedUsers = new UserList(CAPI.ovr_LaunchInvitePanelFlowResult_GetInvitedUsers(o));
        }
    }
}