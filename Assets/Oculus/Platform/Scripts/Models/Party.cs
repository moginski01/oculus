// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

#pragma warning disable 0618

namespace Oculus.Platform.Models
{
    public class Party
    {
        public readonly ulong ID;

        [Obsolete("Deprecated in favor of InvitedUsersOptional")]
        public readonly UserList InvitedUsers;

        // May be null. Check before using.
        public readonly UserList InvitedUsersOptional;

        [Obsolete("Deprecated in favor of LeaderOptional")]
        public readonly User Leader;

        // May be null. Check before using.
        public readonly User LeaderOptional;

        [Obsolete("Deprecated in favor of RoomOptional")]
        public readonly Room Room;

        /// DEPRECATED. Will be removed from headers at version v51.
        // May be null. Check before using.
        public readonly Room RoomOptional;

        [Obsolete("Deprecated in favor of UsersOptional")]
        public readonly UserList Users;

        // May be null. Check before using.
        public readonly UserList UsersOptional;


        public Party(IntPtr o)
        {
            ID = CAPI.ovr_Party_GetID(o);
            {
                var pointer = CAPI.ovr_Party_GetInvitedUsers(o);
                InvitedUsers = new UserList(pointer);
                if (pointer == IntPtr.Zero)
                    InvitedUsersOptional = null;
                else
                    InvitedUsersOptional = InvitedUsers;
            }
            {
                var pointer = CAPI.ovr_Party_GetLeader(o);
                Leader = new User(pointer);
                if (pointer == IntPtr.Zero)
                    LeaderOptional = null;
                else
                    LeaderOptional = Leader;
            }
            {
                var pointer = CAPI.ovr_Party_GetRoom(o);
                Room = new Room(pointer);
                if (pointer == IntPtr.Zero)
                    RoomOptional = null;
                else
                    RoomOptional = Room;
            }
            {
                var pointer = CAPI.ovr_Party_GetUsers(o);
                Users = new UserList(pointer);
                if (pointer == IntPtr.Zero)
                    UsersOptional = null;
                else
                    UsersOptional = Users;
            }
        }
    }
}