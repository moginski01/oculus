// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class LaunchReportFlowResult
    {
        /// Whether the viewer chose to cancel the report flow.
        public readonly bool DidCancel;

        public readonly ulong UserReportId;


        public LaunchReportFlowResult(IntPtr o)
        {
            DidCancel = CAPI.ovr_LaunchReportFlowResult_GetDidCancel(o);
            UserReportId = CAPI.ovr_LaunchReportFlowResult_GetUserReportId(o);
        }
    }
}