// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System.ComponentModel;

namespace Oculus.Platform
{
    using Description = DescriptionAttribute;

    public enum UserOrdering
    {
        [System.ComponentModel.Description("UNKNOWN")]
        Unknown,

        /// No preference for ordering (could be in any or no order)
        [System.ComponentModel.Description("NONE")]
        None,

        /// Orders by online users first and then offline users. Within each group the
        /// users are ordered alphabetically by display name
        [System.ComponentModel.Description("PRESENCE_ALPHABETICAL")]
        PresenceAlphabetical
    }
}