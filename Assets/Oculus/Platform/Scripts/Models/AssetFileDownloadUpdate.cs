// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class AssetFileDownloadUpdate
    {
        /// DEPRECATED. Use AssetFileDownloadUpdate.GetAssetId().
        public readonly ulong AssetFileId;

        /// ID of the asset file
        public readonly ulong AssetId;

        /// Total number of bytes.
        public readonly ulong BytesTotal;

        /// Number of bytes have been downloaded. -1 If the download hasn't started
        /// yet.
        public readonly long BytesTransferred;

        /// Flag indicating a download is completed.
        public readonly bool Completed;


        public AssetFileDownloadUpdate(IntPtr o)
        {
            AssetFileId = CAPI.ovr_AssetFileDownloadUpdate_GetAssetFileId(o);
            AssetId = CAPI.ovr_AssetFileDownloadUpdate_GetAssetId(o);
            BytesTotal = CAPI.ovr_AssetFileDownloadUpdate_GetBytesTotalLong(o);
            BytesTransferred = CAPI.ovr_AssetFileDownloadUpdate_GetBytesTransferredLong(o);
            Completed = CAPI.ovr_AssetFileDownloadUpdate_GetCompleted(o);
        }
    }
}