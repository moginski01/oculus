// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System.ComponentModel;

namespace Oculus.Platform
{
    using Description = DescriptionAttribute;

    public enum RichPresenceExtraContext
    {
        [System.ComponentModel.Description("UNKNOWN")]
        Unknown,

        /// Display nothing
        [System.ComponentModel.Description("NONE")]
        None,

        /// Display the current amount with the user over the max
        [System.ComponentModel.Description("CURRENT_CAPACITY")]
        CurrentCapacity,

        /// Display how long ago the match/game/race/etc started
        [System.ComponentModel.Description("STARTED_AGO")]
        StartedAgo,

        /// Display how soon the match/game/race/etc will end
        [System.ComponentModel.Description("ENDING_IN")]
        EndingIn,

        /// Display that this user is looking for a match
        [System.ComponentModel.Description("LOOKING_FOR_A_MATCH")]
        LookingForAMatch
    }
}