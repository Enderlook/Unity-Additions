﻿using Enderlook.Unity.Utils;

using System;

using UnityEngine;

namespace Enderlook.Unity.Components.ScriptableSound.Modifiers
{
    [Serializable, CreateAssetMenu(fileName = "Volume", menuName = "Enderlook/Scriptable Sound/Modifiers/Volume")]
    public class VolumeModifier : SoundModifier
    {
        [SerializeField, Tooltip("Volume multiplier.")]
        private float volumeMultiplier = 1;

        private float oldVolume;

        public override void ModifyAudioSource(AudioSource audioSource)
        {
            oldVolume = audioSource.volume;
            audioSource.volume *= volumeMultiplier;
        }

        public override void BackToNormalAudioSource(AudioSource audioSource) => audioSource.volume = oldVolume;

        public override SoundModifier CreatePrototype()
        {
            VolumeModifier prototype = CreateInstance<VolumeModifier>();
            prototype.name = PrototypeHelper.GetPrototypeNameOf(prototype);
            prototype.volumeMultiplier = volumeMultiplier;
            return prototype;
        }
    }
}