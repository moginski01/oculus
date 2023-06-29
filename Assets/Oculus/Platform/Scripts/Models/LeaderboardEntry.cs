// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

#pragma warning disable 0618

namespace Oculus.Platform.Models
{
    public class LeaderboardEntry
    {
        public readonly string DisplayScore;
        public readonly byte[] ExtraData;
        public readonly ulong ID;
        public readonly int Rank;
        public readonly long Score;

        [Obsolete("Deprecated in favor of SupplementaryMetricOptional")]
        public readonly SupplementaryMetric SupplementaryMetric;

        // May be null. Check before using.
        public readonly SupplementaryMetric SupplementaryMetricOptional;
        public readonly DateTime Timestamp;
        public readonly User User;


        public LeaderboardEntry(IntPtr o)
        {
            DisplayScore = CAPI.ovr_LeaderboardEntry_GetDisplayScore(o);
            ExtraData = CAPI.ovr_LeaderboardEntry_GetExtraData(o);
            ID = CAPI.ovr_LeaderboardEntry_GetID(o);
            Rank = CAPI.ovr_LeaderboardEntry_GetRank(o);
            Score = CAPI.ovr_LeaderboardEntry_GetScore(o);
            {
                var pointer = CAPI.ovr_LeaderboardEntry_GetSupplementaryMetric(o);
                SupplementaryMetric = new SupplementaryMetric(pointer);
                if (pointer == IntPtr.Zero)
                    SupplementaryMetricOptional = null;
                else
                    SupplementaryMetricOptional = SupplementaryMetric;
            }
            Timestamp = CAPI.ovr_LeaderboardEntry_GetTimestamp(o);
            User = new User(CAPI.ovr_LeaderboardEntry_GetUser(o));
        }
    }

    public class LeaderboardEntryList : DeserializableList<LeaderboardEntry>
    {
        public readonly ulong TotalCount;

        public LeaderboardEntryList(IntPtr a)
        {
            var count = (int)CAPI.ovr_LeaderboardEntryArray_GetSize(a);
            _Data = new List<LeaderboardEntry>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new LeaderboardEntry(CAPI.ovr_LeaderboardEntryArray_GetElement(a, (UIntPtr)i)));

            TotalCount = CAPI.ovr_LeaderboardEntryArray_GetTotalCount(a);
            _PreviousUrl = CAPI.ovr_LeaderboardEntryArray_GetPreviousUrl(a);
            _NextUrl = CAPI.ovr_LeaderboardEntryArray_GetNextUrl(a);
        }
    }
}