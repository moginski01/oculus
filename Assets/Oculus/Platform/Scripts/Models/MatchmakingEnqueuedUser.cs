// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

#pragma warning disable 0618

namespace Oculus.Platform.Models
{
    /// DEPRECATED. Will be removed from headers at version v51.
    public class MatchmakingEnqueuedUser
    {
        /// DEPRECATED. Will be removed from headers at version v51.
        public readonly Dictionary<string, string> CustomData;

        [Obsolete("Deprecated in favor of UserOptional")]
        public readonly User User;

        /// DEPRECATED. Will be removed from headers at version v51.
        // May be null. Check before using.
        public readonly User UserOptional;


        public MatchmakingEnqueuedUser(IntPtr o)
        {
            CustomData = CAPI.DataStoreFromNative(CAPI.ovr_MatchmakingEnqueuedUser_GetCustomData(o));
            {
                var pointer = CAPI.ovr_MatchmakingEnqueuedUser_GetUser(o);
                User = new User(pointer);
                if (pointer == IntPtr.Zero)
                    UserOptional = null;
                else
                    UserOptional = User;
            }
        }
    }

    public class MatchmakingEnqueuedUserList : DeserializableList<MatchmakingEnqueuedUser>
    {
        public MatchmakingEnqueuedUserList(IntPtr a)
        {
            var count = (int)CAPI.ovr_MatchmakingEnqueuedUserArray_GetSize(a);
            _Data = new List<MatchmakingEnqueuedUser>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new MatchmakingEnqueuedUser(CAPI.ovr_MatchmakingEnqueuedUserArray_GetElement(a, (UIntPtr)i)));
        }
    }
}