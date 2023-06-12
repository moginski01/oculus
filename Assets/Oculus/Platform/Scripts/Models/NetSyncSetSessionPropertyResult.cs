// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class NetSyncSetSessionPropertyResult
    {
        /// Which session the operation was modifying
        public readonly NetSyncSession Session;


        public NetSyncSetSessionPropertyResult(IntPtr o)
        {
            Session = new NetSyncSession(CAPI.ovr_NetSyncSetSessionPropertyResult_GetSession(o));
        }
    }
}