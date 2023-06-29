// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System.ComponentModel;

namespace Oculus.Platform
{
    using Description = DescriptionAttribute;

    public enum ChallengeVisibility
    {
        [System.ComponentModel.Description("UNKNOWN")]
        Unknown,

        /// Only those invited can participate in it. Everyone can see it
        [System.ComponentModel.Description("INVITE_ONLY")]
        InviteOnly,

        /// Everyone can participate and see this challenge
        [System.ComponentModel.Description("PUBLIC")]
        Public,

        /// Only those invited can participate and see this challenge
        [System.ComponentModel.Description("PRIVATE")]
        Private
    }
}