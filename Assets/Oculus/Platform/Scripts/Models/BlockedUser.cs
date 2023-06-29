// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

namespace Oculus.Platform.Models
{
    public class BlockedUser
    {
        /// user ID that has been blocked by the logged in user
        public readonly ulong Id;


        public BlockedUser(IntPtr o)
        {
            Id = CAPI.ovr_BlockedUser_GetId(o);
        }
    }

    public class BlockedUserList : DeserializableList<BlockedUser>
    {
        public BlockedUserList(IntPtr a)
        {
            var count = (int)CAPI.ovr_BlockedUserArray_GetSize(a);
            _Data = new List<BlockedUser>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new BlockedUser(CAPI.ovr_BlockedUserArray_GetElement(a, (UIntPtr)i)));

            _NextUrl = CAPI.ovr_BlockedUserArray_GetNextUrl(a);
        }
    }
}