﻿using Enderlook.Unity.Attributes;
using Enderlook.Unity.Components.ScriptableSound.Modifiers;
using Enderlook.Unity.Utils;

using System;
using System.Linq;

using UnityEngine;

namespace Enderlook.Unity.Components.ScriptableSound
{
    [Serializable, CreateAssetMenu(fileName = "Sound Clip", menuName = "Enderlook/Scriptable Sound/Sound Clip")]
    public class SoundClip : Sound
    {
#pragma warning disable CS0649
        [SerializeField, Tooltip("Audioclip to play.\nModifiers doesn't work if played in Inspector."), PlayAudioClip]
        private AudioClip audioClip;

        [SerializeField, Min(-1), Tooltip("Amount of times it will play when called Play method. If 0 doesn't play. If negative it loops forever.")]
        private int playsAmount = 1;

        private int remainingPlays;

        [SerializeField, Tooltip("Modifiers to AudioSource."), Expandable]
        private SoundModifier[] modifiers;
#pragma warning restore

        private bool HasEnoughPlays()
        {
            bool can = remainingPlays > 0 || playsAmount == -1;
            remainingPlays--;
            return can;
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            if (ShouldChangeSound)
                if (HasEnoughPlays())
                {
                    AudioSource audioSource = soundConfiguration.audioSource;
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    Array.ForEach(modifiers, e => e.ModifyAudioSource(audioSource));
                }
                else
                {
                    IsPlaying = false;
                    BackToNormalAudioSource();
                }
        }

        private void BackToNormalAudioSource()
        {
            AudioSource audioSource = soundConfiguration.audioSource;
            for (int i = modifiers.Length - 1; i >= 0; i--)
                modifiers[i].BackToNormalAudioSource(audioSource);
            soundConfiguration.EndCallback();
        }

        public override void Stop()
        {
            BackToNormalAudioSource();
            base.Stop();
        }

        public override void Play()
        {
            if (IsPlaying)
                BackToNormalAudioSource();
            remainingPlays = playsAmount;
            soundConfiguration.audioSource.Stop();
            base.Play();
        }

        public override Sound CreatePrototype()
        {
            SoundClip prototype = CreateInstance<SoundClip>();
            prototype.name = PrototypeHelper.GetPrototypeNameOf(prototype);
            prototype.audioClip = audioClip;
            prototype.modifiers = modifiers.Select(e => e.CreatePrototype()).ToArray();
            prototype.playsAmount = playsAmount;
            return prototype;
        }

#if UNITY_EDITOR
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnValidate()
        {
            foreach (SoundModifier modifier in modifiers)
                if (modifier != null)
                    modifier.Validate(this);
        }
#endif
    }
}