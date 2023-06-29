// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

namespace Oculus.Platform.Models
{
    /// DEPRECATED. Will be removed from headers at version v51.
    public class RoomInviteNotification
    {
        /// DEPRECATED. Will be removed from headers at version v51.
        public readonly ulong ID;

        /// DEPRECATED. Will be removed from headers at version v51.
        public readonly ulong RoomID;

        /// DEPRECATED. Will be removed from headers at version v51.
        public readonly ulong SenderID;

        /// DEPRECATED. Will be removed from headers at version v51.
        public readonly DateTime SentTime;


        public RoomInviteNotification(IntPtr o)
        {
            ID = CAPI.ovr_RoomInviteNotification_GetID(o);
            RoomID = CAPI.ovr_RoomInviteNotification_GetRoomID(o);
            SenderID = CAPI.ovr_RoomInviteNotification_GetSenderID(o);
            SentTime = CAPI.ovr_RoomInviteNotification_GetSentTime(o);
        }
    }

    public class RoomInviteNotificationList : DeserializableList<RoomInviteNotification>
    {
        public RoomInviteNotificationList(IntPtr a)
        {
            var count = (int)CAPI.ovr_RoomInviteNotificationArray_GetSize(a);
            _Data = new List<RoomInviteNotification>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new RoomInviteNotification(CAPI.ovr_RoomInviteNotificationArray_GetElement(a, (UIntPtr)i)));

            _NextUrl = CAPI.ovr_RoomInviteNotificationArray_GetNextUrl(a);
        }
    }
}