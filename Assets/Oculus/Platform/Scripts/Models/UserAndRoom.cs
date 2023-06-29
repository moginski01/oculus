// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

#pragma warning disable 0618

namespace Oculus.Platform.Models
{
    /// DEPRECATED. Will be removed from headers at version v51.
    public class UserAndRoom
    {
        [Obsolete("Deprecated in favor of RoomOptional")]
        public readonly Room Room;

        /// DEPRECATED. Will be removed from headers at version v51.
        // May be null. Check before using.
        public readonly Room RoomOptional;

        public readonly User User;


        public UserAndRoom(IntPtr o)
        {
            {
                var pointer = CAPI.ovr_UserAndRoom_GetRoom(o);
                Room = new Room(pointer);
                if (pointer == IntPtr.Zero)
                    RoomOptional = null;
                else
                    RoomOptional = Room;
            }
            User = new User(CAPI.ovr_UserAndRoom_GetUser(o));
        }
    }

    public class UserAndRoomList : DeserializableList<UserAndRoom>
    {
        public UserAndRoomList(IntPtr a)
        {
            var count = (int)CAPI.ovr_UserAndRoomArray_GetSize(a);
            _Data = new List<UserAndRoom>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new UserAndRoom(CAPI.ovr_UserAndRoomArray_GetElement(a, (UIntPtr)i)));

            _NextUrl = CAPI.ovr_UserAndRoomArray_GetNextUrl(a);
        }
    }
}