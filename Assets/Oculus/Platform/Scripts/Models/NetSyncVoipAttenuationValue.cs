// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System;
using System.Collections.Generic;

namespace Oculus.Platform.Models
{
    public class NetSyncVoipAttenuationValue
    {
        /// decibel fall-off value
        public readonly float Decibels;

        /// The starting distance of this attenuation value
        public readonly float Distance;


        public NetSyncVoipAttenuationValue(IntPtr o)
        {
            Decibels = CAPI.ovr_NetSyncVoipAttenuationValue_GetDecibels(o);
            Distance = CAPI.ovr_NetSyncVoipAttenuationValue_GetDistance(o);
        }
    }

    public class NetSyncVoipAttenuationValueList : DeserializableList<NetSyncVoipAttenuationValue>
    {
        public NetSyncVoipAttenuationValueList(IntPtr a)
        {
            var count = (int)CAPI.ovr_NetSyncVoipAttenuationValueArray_GetSize(a);
            _Data = new List<NetSyncVoipAttenuationValue>(count);
            for (var i = 0; i < count; i++)
                _Data.Add(new NetSyncVoipAttenuationValue(
                    CAPI.ovr_NetSyncVoipAttenuationValueArray_GetElement(a, (UIntPtr)i)));
        }
    }
}