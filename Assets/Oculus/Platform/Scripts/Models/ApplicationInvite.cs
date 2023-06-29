// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

#pragma warning disable 0618

namespace Oculus.Platform.Models
{
    public class ApplicationInvite
    {
        [Obsolete("Deprecated in favor of DestinationOptional")]
        public readonly Destination Destination;

        // May be null. Check before using.
        public readonly Destination DestinationOptional;
        public readonly ulong ID;
        public readonly bool IsActive;
        public readonly string LobbySessionId;
        public readonly string MatchSessionId;

        [Obsolete("Deprecated in favor of RecipientOptional")]
        public readonly User Recipient;

        // May be null. Check before using.
        public readonly User RecipientOptional;


        public ApplicationInvite(IntPtr o)
        {
            {
                var pointer = CAPI.ovr_ApplicationInvite_GetDestination(o);
                Destination = new Destination(pointer);
                if (pointer == IntPtr.Zero)
                    DestinationOptional = null;
                else
                    DestinationOptional = Destination;
            }
            ID = CAPI.ovr_ApplicationInvite_GetID(o);
            IsActive = CAPI.ovr_ApplicationInvite_GetIsActive(o);
            LobbySessionId = CAPI.ovr_ApplicationInvite_GetLobbySessionId(o);
            MatchSessionId = CAPI.ovr_ApplicationInvite_GetMatchSessionId(o);
            {
                var pointer = CAPI.ovr_ApplicationInvite_GetRecipient(o);
                Recipient = new User(pointer);
                if (pointer == IntPtr.Zero)
                    RecipientOptional = null;
                else
                    RecipientOptional = Recipient;
            }
        }
    }

    public class ApplicationInviteList : DeserializableList<ApplicationInvite>
    {
        public ApplicationInviteList(IntPtr a)
        {
            var count = (int)CAPI.ovr_ApplicationInviteArray_GetSize(a);
            _Data = new List<ApplicationInvite>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new ApplicationInvite(CAPI.ovr_ApplicationInviteArray_GetElement(a, (UIntPtr)i)));

            _NextUrl = CAPI.ovr_ApplicationInviteArray_GetNextUrl(a);
        }
    }
}