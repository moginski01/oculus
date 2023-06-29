// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class AbuseReportRecording
    {
        /// A UUID associated with the Abuse Report recording.
        public readonly string RecordingUuid;


        public AbuseReportRecording(IntPtr o)
        {
            RecordingUuid = CAPI.ovr_AbuseReportRecording_GetRecordingUuid(o);
        }
    }
}