using Enderlook.Unity.Components.Destroy;

using System;

using UnityEngine;
using UnityEngine.Audio;

namespace Enderlook.Unity.Components
{
    /// <summary>
    /// Helper methods for <see cref="AudioSource"/>.
    /// </summary>
    public static class AudioSourceUtils
    {
        private const string BASE_NAME = nameof(AudioSourceUtils) + "." + nameof(PlayAndDestroy);
        private const string VOLUME_OUT_OF_RANGE = "Must be between 0 and 1.";
        private const string PITCH_OUT_OF_RANGE = "Must be between -3 and 3.";

        private static AudioSource CreateBase(AudioClip clip, DestroyWhenAudioSourceEnds.Mode destroyMode)
        {
            GameObject gameObject = new GameObject($"{BASE_NAME}.{clip.name}");
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            DestroyWhenAudioSourceEnds.AddComponent(gameObject, destroyMode);
            audioSource.Play();
            return audioSource;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        ///  <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        public static void PlayAndDestroy(AudioClip clip, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));

            CreateBase(clip, destroyMode);
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="position">Position where <see cref="AudioSource"/> will be instanced.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        public static void PlayAndDestroy(AudioClip clip, Vector3 position, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.transform.position = position;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="position">Position where <see cref="AudioSource"/> will be instanced.</param>
        /// <param name="volume">Volume of <see cref="AudioSource"/>. Between 0 and 1.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="volume"/> isn't between 0 and 1.</exception>
        public static void PlayAndDestroy(AudioClip clip, Vector3 position, float volume, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (volume > 1 || volume < 0) throw new ArgumentOutOfRangeException(nameof(volume), volume, VOLUME_OUT_OF_RANGE);

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.transform.position = position;
            audioSource.volume = volume;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="position">Position where <see cref="AudioSource"/> will be instanced.</param>
        /// <param name="volume">Volume of <see cref="AudioSource"/>. Between 0 and 1.</param>
        /// <param name="pitch">Pitch of <see cref="AudioSource"/>. Between -3 and 3.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="volume"/> isn't between 0 and 1, or when <paramref name="pitch"/> isn't between -3 and 3.</exception>
        public static void PlayAndDestroy(AudioClip clip, Vector3 position, float volume, float pitch, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (volume > 1 || volume < 0) throw new ArgumentOutOfRangeException(nameof(volume), volume, VOLUME_OUT_OF_RANGE);
            if (pitch > 1 || pitch < 0) throw new ArgumentOutOfRangeException(nameof(pitch), pitch, PITCH_OUT_OF_RANGE);

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.transform.position = position;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="position">Position where <see cref="AudioSource"/> will be instanced.</param>
        /// <param name="volume">Volume of <see cref="AudioSource"/>. Between 0 and 1.</param>
        /// <param name="pitch">Pitch of <see cref="AudioSource"/>. Between -3 and 3.</param>
        /// <param name="audioMixerGroup">Mixer group used by <see cref="AudioSource"/>.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> or <paramref name="audioMixerGroup"/> are <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="volume"/> isn't between 0 and 1, or when <paramref name="pitch"/> isn't between -3 and 3.</exception>
        public static void PlayAndDestroy(AudioClip clip, Vector3 position = default, float volume = 1, float pitch = 1, AudioMixerGroup audioMixerGroup = null, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (volume > 1 || volume < 0) throw new ArgumentOutOfRangeException(nameof(volume), volume, VOLUME_OUT_OF_RANGE);
            if (pitch > 1 || pitch < 0) throw new ArgumentOutOfRangeException(nameof(pitch), pitch, PITCH_OUT_OF_RANGE);

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.transform.position = position;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="volume">Volume of <see cref="AudioSource"/>. Between 0 and 1.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="volume"/> isn't between 0 and 1.</exception>
        public static void PlayAndDestroy(AudioClip clip, float volume, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (volume > 1 || volume < 0) throw new ArgumentOutOfRangeException(nameof(volume), volume, VOLUME_OUT_OF_RANGE);

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.volume = volume;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="volume">Volume of <see cref="AudioSource"/>. Between 0 and 1.</param>
        /// <param name="pitch">Pitch of <see cref="AudioSource"/>. Between -3 and 3.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="volume"/> isn't between 0 and 1, or when <paramref name="pitch"/> isn't between -3 and 3.</exception>
        public static void PlayAndDestroy(AudioClip clip, float volume, float pitch, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (volume > 1 || volume < 0) throw new ArgumentOutOfRangeException(nameof(volume), volume, VOLUME_OUT_OF_RANGE);
            if (pitch > 1 || pitch < 0) throw new ArgumentOutOfRangeException(nameof(pitch), pitch, PITCH_OUT_OF_RANGE);

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.volume = volume;
            audioSource.pitch = pitch;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="volume">Volume of <see cref="AudioSource"/>. Between 0 and 1.</param>
        /// <param name="pitch">Pitch of <see cref="AudioSource"/>. Between -3 and 3.</param>
        /// <param name="audioMixerGroup">Mixer group used by <see cref="AudioSource"/>.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="volume"/> isn't between 0 and 1, or when <paramref name="pitch"/> isn't between -3 and 3.</exception>
        public static void PlayAndDestroy(AudioClip clip, float volume, float pitch, AudioMixerGroup audioMixerGroup, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (volume > 1 || volume < 0) throw new ArgumentOutOfRangeException(nameof(volume), volume, VOLUME_OUT_OF_RANGE);
            if (pitch > 1 || pitch < 0) throw new ArgumentOutOfRangeException(nameof(pitch), pitch, PITCH_OUT_OF_RANGE);

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="audioMixerGroup">Mixer group used by <see cref="AudioSource"/>.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        public static void PlayAndDestroy(AudioClip clip, AudioMixerGroup audioMixerGroup, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="position">Position where <see cref="AudioSource"/> will be instanced.</param>
        /// <param name="volume">Volume of <see cref="AudioSource"/>. Between 0 and 1.</param>
        /// <param name="audioMixerGroup">Mixer group used by <see cref="AudioSource"/>.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="volume"/> isn't between 0 and 1.</exception>
        public static void PlayAndDestroy(AudioClip clip, Vector3 position, float volume, AudioMixerGroup audioMixerGroup, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (volume > 1 || volume < 0) throw new ArgumentOutOfRangeException(nameof(volume), volume, VOLUME_OUT_OF_RANGE);

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.transform.position = position;
            audioSource.volume = volume;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="position">Position where <see cref="AudioSource"/> will be instanced.</param>
        /// <param name="audioMixerGroup">Mixer group used by <see cref="AudioSource"/>.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        public static void PlayAndDestroy(AudioClip clip, Vector3 position, AudioMixerGroup audioMixerGroup, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.transform.position = position;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }

        /// <summary>
        /// Creates and <see cref="AudioSource"/> to play <paramref name="clip"/> and destroy it when ends playing.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="volume">Volume of <see cref="AudioSource"/>. Between 0 and 1.</param>
        /// <param name="audioMixerGroup">Mixer group used by <see cref="AudioSource"/>.</param>
        /// <param name="destroyMode">How destroy time will be calculated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="clip"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="volume"/> isn't between 0 and 1.</exception>
        public static void PlayAndDestroy(AudioClip clip, float volume, AudioMixerGroup audioMixerGroup, DestroyWhenAudioSourceEnds.Mode destroyMode = DestroyWhenAudioSourceEnds.Mode.CheckOnStartForClipLength)
        {
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (volume > 1 || volume < 0) throw new ArgumentOutOfRangeException(nameof(volume), volume, VOLUME_OUT_OF_RANGE);

            AudioSource audioSource = CreateBase(clip, destroyMode);
            audioSource.volume = volume;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }
}
