// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System.ComponentModel;

namespace Oculus.Platform
{
    using Description = DescriptionAttribute;

    public enum LivestreamingStartStatus
    {
        [System.ComponentModel.Description("SUCCESS")]
        Success = 1,

        [System.ComponentModel.Description("UNKNOWN")]
        Unknown = 0,

        [System.ComponentModel.Description("NO_PACKAGE_SET")]
        NoPackageSet = -1,

        [System.ComponentModel.Description("NO_FB_CONNECT")]
        NoFbConnect = -2,

        [System.ComponentModel.Description("NO_SESSION_ID")]
        NoSessionId = -3,

        [System.ComponentModel.Description("MISSING_PARAMETERS")]
        MissingParameters = -4
    }
}