// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    /// DEPRECATED. Will be removed from headers at version v51.
    public class MatchmakingAdminSnapshot
    {
        /// DEPRECATED. Will be removed from headers at version v51.
        public readonly MatchmakingAdminSnapshotCandidateList Candidates;

        /// DEPRECATED. Will be removed from headers at version v51.
        public readonly double MyCurrentThreshold;


        public MatchmakingAdminSnapshot(IntPtr o)
        {
            Candidates = new MatchmakingAdminSnapshotCandidateList(CAPI.ovr_MatchmakingAdminSnapshot_GetCandidates(o));
            MyCurrentThreshold = CAPI.ovr_MatchmakingAdminSnapshot_GetMyCurrentThreshold(o);
        }
    }
}