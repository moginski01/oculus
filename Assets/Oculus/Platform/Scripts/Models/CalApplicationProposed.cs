// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    /// DEPRECATED. Will be removed from headers at version v51.
    public class CalApplicationProposed
    {
        public readonly ulong ID;


        public CalApplicationProposed(IntPtr o)
        {
            ID = CAPI.ovr_CalApplicationProposed_GetID(o);
        }
    }
}