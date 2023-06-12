// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class InvitePanelResultInfo
    {
        /// A boolean for whether or not any invites has been sent.
        public readonly bool InvitesSent;


        public InvitePanelResultInfo(IntPtr o)
        {
            InvitesSent = CAPI.ovr_InvitePanelResultInfo_GetInvitesSent(o);
        }
    }
}