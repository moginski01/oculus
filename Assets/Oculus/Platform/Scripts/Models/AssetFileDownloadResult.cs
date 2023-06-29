// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class AssetFileDownloadResult
    {
        /// ID of the asset file
        public readonly ulong AssetId;

        /// File path of the asset file.
        public readonly string Filepath;


        public AssetFileDownloadResult(IntPtr o)
        {
            AssetId = CAPI.ovr_AssetFileDownloadResult_GetAssetId(o);
            Filepath = CAPI.ovr_AssetFileDownloadResult_GetFilepath(o);
        }
    }
}