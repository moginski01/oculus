// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform
{
    public class AdvancedAbuseReportOptions
    {
        private readonly IntPtr Handle;

        public AdvancedAbuseReportOptions()
        {
            Handle = CAPI.ovr_AdvancedAbuseReportOptions_Create();
        }

        public void SetDeveloperDefinedContext(string key, string value)
        {
            CAPI.ovr_AdvancedAbuseReportOptions_SetDeveloperDefinedContextString(Handle, key, value);
        }

        public void ClearDeveloperDefinedContext()
        {
            CAPI.ovr_AdvancedAbuseReportOptions_ClearDeveloperDefinedContext(Handle);
        }

        /// If report_type is content, a string representing the type of content being
        /// reported. This should correspond to the object_type string used in the UI
        public void SetObjectType(string value)
        {
            CAPI.ovr_AdvancedAbuseReportOptions_SetObjectType(Handle, value);
        }

        public void SetReportType(AbuseReportType value)
        {
            CAPI.ovr_AdvancedAbuseReportOptions_SetReportType(Handle, value);
        }

        public void AddSuggestedUser(ulong userID)
        {
            CAPI.ovr_AdvancedAbuseReportOptions_AddSuggestedUser(Handle, userID);
        }

        public void ClearSuggestedUsers()
        {
            CAPI.ovr_AdvancedAbuseReportOptions_ClearSuggestedUsers(Handle);
        }

        public void SetVideoMode(AbuseReportVideoMode value)
        {
            CAPI.ovr_AdvancedAbuseReportOptions_SetVideoMode(Handle, value);
        }


        /// For passing to native C
        public static explicit operator IntPtr(AdvancedAbuseReportOptions options)
        {
            return options != null ? options.Handle : IntPtr.Zero;
        }

        ~AdvancedAbuseReportOptions()
        {
            CAPI.ovr_AdvancedAbuseReportOptions_Destroy(Handle);
        }
    }
}