// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;

namespace Oculus.Platform.Models
{
    public class AchievementUpdate
    {
        public readonly bool JustUnlocked;
        public readonly string Name;


        public AchievementUpdate(IntPtr o)
        {
            JustUnlocked = CAPI.ovr_AchievementUpdate_GetJustUnlocked(o);
            Name = CAPI.ovr_AchievementUpdate_GetName(o);
        }
    }
}