// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

namespace Oculus.Platform.Models
{
    public class ChallengeEntry
    {
        public readonly string DisplayScore;
        public readonly byte[] ExtraData;
        public readonly ulong ID;
        public readonly int Rank;
        public readonly long Score;
        public readonly DateTime Timestamp;
        public readonly User User;


        public ChallengeEntry(IntPtr o)
        {
            DisplayScore = CAPI.ovr_ChallengeEntry_GetDisplayScore(o);
            ExtraData = CAPI.ovr_ChallengeEntry_GetExtraData(o);
            ID = CAPI.ovr_ChallengeEntry_GetID(o);
            Rank = CAPI.ovr_ChallengeEntry_GetRank(o);
            Score = CAPI.ovr_ChallengeEntry_GetScore(o);
            Timestamp = CAPI.ovr_ChallengeEntry_GetTimestamp(o);
            User = new User(CAPI.ovr_ChallengeEntry_GetUser(o));
        }
    }

    public class ChallengeEntryList : DeserializableList<ChallengeEntry>
    {
        public readonly ulong TotalCount;

        public ChallengeEntryList(IntPtr a)
        {
            var count = (int)CAPI.ovr_ChallengeEntryArray_GetSize(a);
            _Data = new List<ChallengeEntry>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new ChallengeEntry(CAPI.ovr_ChallengeEntryArray_GetElement(a, (UIntPtr)i)));

            TotalCount = CAPI.ovr_ChallengeEntryArray_GetTotalCount(a);
            _PreviousUrl = CAPI.ovr_ChallengeEntryArray_GetPreviousUrl(a);
            _NextUrl = CAPI.ovr_ChallengeEntryArray_GetNextUrl(a);
        }
    }
}