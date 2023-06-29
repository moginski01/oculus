// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class PartyID
    {
        public readonly ulong ID;


        public PartyID(IntPtr o)
        {
            ID = CAPI.ovr_PartyID_GetID(o);
        }
    }
}