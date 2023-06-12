// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
  /// DEPRECATED. Do not add new requests using this. Use
  /// launch_report_flow_result instead.
  public class UserReportID
    {
        /// Whether the viewer chose to cancel the report flow.
        public readonly bool DidCancel;

        public readonly ulong ID;


        public UserReportID(IntPtr o)
        {
            DidCancel = CAPI.ovr_UserReportID_GetDidCancel(o);
            ID = CAPI.ovr_UserReportID_GetID(o);
        }
    }
}