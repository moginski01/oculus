// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

namespace Oculus.Platform.Models
{
    public class InstalledApplication
    {
        public readonly string ApplicationId;
        public readonly string PackageName;
        public readonly string Status;
        public readonly int VersionCode;
        public readonly string VersionName;


        public InstalledApplication(IntPtr o)
        {
            ApplicationId = CAPI.ovr_InstalledApplication_GetApplicationId(o);
            PackageName = CAPI.ovr_InstalledApplication_GetPackageName(o);
            Status = CAPI.ovr_InstalledApplication_GetStatus(o);
            VersionCode = CAPI.ovr_InstalledApplication_GetVersionCode(o);
            VersionName = CAPI.ovr_InstalledApplication_GetVersionName(o);
        }
    }

    public class InstalledApplicationList : DeserializableList<InstalledApplication>
    {
        public InstalledApplicationList(IntPtr a)
        {
            var count = (int)CAPI.ovr_InstalledApplicationArray_GetSize(a);
            _Data = new List<InstalledApplication>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new InstalledApplication(CAPI.ovr_InstalledApplicationArray_GetElement(a, (UIntPtr)i)));
        }
    }
}