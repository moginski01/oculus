// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

namespace Oculus.Platform.Models
{
    public class LinkedAccount
    {
        /// Access token of the linked account.
        public readonly string AccessToken;

        /// Service provider with which the linked account is associated.
        public readonly ServiceProvider ServiceProvider;

        /// User ID of the linked account.
        public readonly string UserId;


        public LinkedAccount(IntPtr o)
        {
            AccessToken = CAPI.ovr_LinkedAccount_GetAccessToken(o);
            ServiceProvider = CAPI.ovr_LinkedAccount_GetServiceProvider(o);
            UserId = CAPI.ovr_LinkedAccount_GetUserId(o);
        }
    }

    public class LinkedAccountList : DeserializableList<LinkedAccount>
    {
        public LinkedAccountList(IntPtr a)
        {
            var count = (int)CAPI.ovr_LinkedAccountArray_GetSize(a);
            _Data = new List<LinkedAccount>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new LinkedAccount(CAPI.ovr_LinkedAccountArray_GetElement(a, (UIntPtr)i)));
        }
    }
}