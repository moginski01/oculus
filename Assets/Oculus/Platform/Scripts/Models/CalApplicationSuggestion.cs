// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

namespace Oculus.Platform.Models
{
    /// DEPRECATED. Will be removed from headers at version v51.
    public class CalApplicationSuggestion
    {
        /// Application ID of the suggested app.
        public readonly ulong ID;

        /// Localized, privacy aware social context string to go with the app
        /// suggestion. Intended to be directly displayed in UI.
        public readonly string SocialContext;


        public CalApplicationSuggestion(IntPtr o)
        {
            ID = CAPI.ovr_CalApplicationSuggestion_GetID(o);
            SocialContext = CAPI.ovr_CalApplicationSuggestion_GetSocialContext(o);
        }
    }

    public class CalApplicationSuggestionList : DeserializableList<CalApplicationSuggestion>
    {
        public CalApplicationSuggestionList(IntPtr a)
        {
            var count = (int)CAPI.ovr_CalApplicationSuggestionArray_GetSize(a);
            _Data = new List<CalApplicationSuggestion>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(
                    new CalApplicationSuggestion(CAPI.ovr_CalApplicationSuggestionArray_GetElement(a, (UIntPtr)i)));
        }
    }
}