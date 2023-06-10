// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System.ComponentModel;

namespace Oculus.Platform
{
    using Description = DescriptionAttribute;

    public enum PlatformInitializeResult
    {
        [System.ComponentModel.Description("SUCCESS")]
        Success = 0,

        [System.ComponentModel.Description("UNINITIALIZED")]
        Uninitialized = -1,

        [System.ComponentModel.Description("PRE_LOADED")]
        PreLoaded = -2,

        [System.ComponentModel.Description("FILE_INVALID")]
        FileInvalid = -3,

        [System.ComponentModel.Description("SIGNATURE_INVALID")]
        SignatureInvalid = -4,

        [System.ComponentModel.Description("UNABLE_TO_VERIFY")]
        UnableToVerify = -5,

        [System.ComponentModel.Description("VERSION_MISMATCH")]
        VersionMismatch = -6,

        [System.ComponentModel.Description("UNKNOWN")]
        Unknown = -7,

        [System.ComponentModel.Description("INVALID_CREDENTIALS")]
        InvalidCredentials = -8,

        [System.ComponentModel.Description("NOT_ENTITLED")]
        NotEntitled = -9
    }
}