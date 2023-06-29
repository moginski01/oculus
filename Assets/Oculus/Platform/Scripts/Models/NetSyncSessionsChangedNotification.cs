// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class NetSyncSessionsChangedNotification
    {
        public readonly long ConnectionId;

        /// The new list of sessions
        public readonly NetSyncSessionList Sessions;


        public NetSyncSessionsChangedNotification(IntPtr o)
        {
            ConnectionId = CAPI.ovr_NetSyncSessionsChangedNotification_GetConnectionId(o);
            Sessions = new NetSyncSessionList(CAPI.ovr_NetSyncSessionsChangedNotification_GetSessions(o));
        }
    }
}