// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform
{
    public class AvatarEditorOptions
    {
        private readonly IntPtr Handle;

        public AvatarEditorOptions()
        {
            Handle = CAPI.ovr_AvatarEditorOptions_Create();
        }

        /// Optional: Override for where the request is coming from.
        public void SetSourceOverride(string value)
        {
            CAPI.ovr_AvatarEditorOptions_SetSourceOverride(Handle, value);
        }


        /// For passing to native C
        public static explicit operator IntPtr(AvatarEditorOptions options)
        {
            return options != null ? options.Handle : IntPtr.Zero;
        }

        ~AvatarEditorOptions()
        {
            CAPI.ovr_AvatarEditorOptions_Destroy(Handle);
        }
    }
}