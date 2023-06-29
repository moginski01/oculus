// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class OrgScopedID
    {
        public readonly ulong ID;


        public OrgScopedID(IntPtr o)
        {
            ID = CAPI.ovr_OrgScopedID_GetID(o);
        }
    }
}