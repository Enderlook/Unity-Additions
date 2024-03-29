﻿using Enderlook.Unity.Attributes;

using System;

using UnityEngine;

namespace Enderlook.Unity.Components.ScriptableSound
{
    [RequireComponent(typeof(AudioSource)), AddComponentMenu("Enderlook/Scriptable Sound/Sound Player")]
    public class SoundPlayer : MonoBehaviour
    {
        private AudioSource audioSource;
#pragma warning disable CS0649
        [SerializeField, Tooltip("List of sounds to play."), Expandable]
        private Sound[] sounds;

        [SerializeField, Tooltip("Determine events that should trigger this to play a sound.")]
        private PlayEvents playOn;
#pragma warning restore CS0649

        private int index;

        /// <summary>
        /// Whenever the last sound used is playing or not.
        /// </summary>
        public bool IsPlaying => sounds[index].IsPlaying;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            for (int i = 0; i < sounds.Length; i++)
                sounds[i] = sounds[i].CreatePrototype();

            if (playOn.OnAwake(out int index))
                Play(index);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Start()
        {
            if (playOn.OnStart(out int index))
                Play(index);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnEnable()
        {
            if (playOn.OnEnable(out int index))
                Play(index);
        }

        /// <summary>
        /// Play <see cref="sounds"/> at <see cref="index"/>.
        /// </summary>
        /// <param name="endCallback"><see cref="Action"/> called after <see cref="sound"/> ends.</param>
        public void Play(int index, Action endCallback = null)
        {
            Sound oldSound = sounds[this.index];
            if (oldSound.IsPlaying)
                oldSound.Stop();

            Sound sound = sounds[index];
            sound.SetConfiguration(new SoundConfiguration(audioSource, endCallback));
            sound.Play();
            this.index = index;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Update() => sounds[index].UpdateBehaviour(Time.deltaTime);

        /// <summary>
        /// Stop <see cref="sounds"/> at <see cref="index"/> from playing.
        /// </summary>
        public void Stop() => sounds[index].Stop();
    }
}