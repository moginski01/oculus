// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

#pragma warning disable 0618

namespace Oculus.Platform.Models
{
    public class Challenge
    {
        /// Was this challenge created by a user or the app developer
        public readonly ChallengeCreationType CreationType;

        /// A displayable string of the challenge's description
        public readonly string Description;

        public readonly DateTime EndDate;
        public readonly ulong ID;

        [Obsolete("Deprecated in favor of InvitedUsersOptional")]
        public readonly UserList InvitedUsers;

        // May be null. Check before using.
        public readonly UserList InvitedUsersOptional;

        /// The leaderboard associated with this challenge
        public readonly Leaderboard Leaderboard;

        [Obsolete("Deprecated in favor of ParticipantsOptional")]
        public readonly UserList Participants;

        // May be null. Check before using.
        public readonly UserList ParticipantsOptional;
        public readonly DateTime StartDate;

        /// A displayable string of the challenge's title
        public readonly string Title;

        /// A enum that specify who can see this challenge
        public readonly ChallengeVisibility Visibility;


        public Challenge(IntPtr o)
        {
            CreationType = CAPI.ovr_Challenge_GetCreationType(o);
            Description = CAPI.ovr_Challenge_GetDescription(o);
            EndDate = CAPI.ovr_Challenge_GetEndDate(o);
            ID = CAPI.ovr_Challenge_GetID(o);
            {
                var pointer = CAPI.ovr_Challenge_GetInvitedUsers(o);
                InvitedUsers = new UserList(pointer);
                if (pointer == IntPtr.Zero)
                    InvitedUsersOptional = null;
                else
                    InvitedUsersOptional = InvitedUsers;
            }
            Leaderboard = new Leaderboard(CAPI.ovr_Challenge_GetLeaderboard(o));
            {
                var pointer = CAPI.ovr_Challenge_GetParticipants(o);
                Participants = new UserList(pointer);
                if (pointer == IntPtr.Zero)
                    ParticipantsOptional = null;
                else
                    ParticipantsOptional = Participants;
            }
            StartDate = CAPI.ovr_Challenge_GetStartDate(o);
            Title = CAPI.ovr_Challenge_GetTitle(o);
            Visibility = CAPI.ovr_Challenge_GetVisibility(o);
        }
    }

    public class ChallengeList : DeserializableList<Challenge>
    {
        public readonly ulong TotalCount;

        public ChallengeList(IntPtr a)
        {
            var count = (int)CAPI.ovr_ChallengeArray_GetSize(a);
            _Data = new List<Challenge>(count);
            for (var i = 0; i < count; i++) _Data.Add(new Challenge(CAPI.ovr_ChallengeArray_GetElement(a, (UIntPtr)i)));

            TotalCount = CAPI.ovr_ChallengeArray_GetTotalCount(a);
            _PreviousUrl = CAPI.ovr_ChallengeArray_GetPreviousUrl(a);
            _NextUrl = CAPI.ovr_ChallengeArray_GetNextUrl(a);
        }
    }
}