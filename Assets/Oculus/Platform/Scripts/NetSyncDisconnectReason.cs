// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System.ComponentModel;

namespace Oculus.Platform
{
    using Description = DescriptionAttribute;

    public enum NetSyncDisconnectReason
    {
        [System.ComponentModel.Description("UNKNOWN")]
        Unknown,

        /// when disconnect was requested
        [System.ComponentModel.Description("LOCAL_TERMINATED")]
        LocalTerminated,

        /// server intentionally closed the connection
        [System.ComponentModel.Description("SERVER_TERMINATED")]
        ServerTerminated,

        /// initial connection never succeeded
        [System.ComponentModel.Description("FAILED")]
        Failed,

        /// network timeout
        [System.ComponentModel.Description("LOST")]
        Lost
    }
}