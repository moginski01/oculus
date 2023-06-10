// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class GroupPresenceLeaveIntent
    {
        /// If populated, the destination the current user wants to leave
        public readonly string DestinationApiName;

        /// If populated, the lobby session the current user wants to leave
        public readonly string LobbySessionId;

        /// If populated, the match session the current user wants to leave
        public readonly string MatchSessionId;


        public GroupPresenceLeaveIntent(IntPtr o)
        {
            DestinationApiName = CAPI.ovr_GroupPresenceLeaveIntent_GetDestinationApiName(o);
            LobbySessionId = CAPI.ovr_GroupPresenceLeaveIntent_GetLobbySessionId(o);
            MatchSessionId = CAPI.ovr_GroupPresenceLeaveIntent_GetMatchSessionId(o);
        }
    }
}