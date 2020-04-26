using UnityEngine;

namespace Enderlook.Unity.Components.Destroy
{
    /// <summary>
    /// Desotry the <see cref="GameObject"/> when the <see cref="AudioSource"/> stop playing.
    /// </summary>
    [RequireComponent(typeof(AudioSource)), AddComponentMenu("Enderlook/Destroyers/Destroy When Audio Source Ends")]
    public class DestroyWhenAudioSourceEnds : MonoBehaviour
    {
        /// <summary>
        /// Determines how will <see cref="AudioSource"/> be checked for destroyment.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// During Start, the current <see cref="AudioClip"/> length will be used to determine when the <see cref="GameObject"/> will be destroyed.
            /// </summary>
            CheckOnStartForClipLength,

            /// <summary>
            /// During Update, <see cref="AudioSource"/> will be check to determine if it's playing or the <see cref="GameObject"/> should be destroyed.
            /// </summary>
            CheckOnUpdateIfIsNotPlaying,

            /// <summary>
            /// During Update, <see cref="AudioSource"/> will be check to determine it was not playing, then playing, and finaly stopped playing to destroy the <see cref="GameObject"/>
            /// </summary>
            CheckOnUpdateIfStartedAndThenStopedPlaying
        }

        [SerializeField, Tooltip("Determines how Audio Source will be check to destroy the Game Object.")]
        private Mode mode;

        private AudioSource audioSource;

        private bool wasPlaying;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (mode == Mode.CheckOnStartForClipLength)
                Destroy(gameObject, audioSource.clip.length);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Update()
        {
            if (mode == Mode.CheckOnUpdateIfIsNotPlaying && !audioSource.isPlaying)
                Destroy(gameObject);
            else if (mode == Mode.CheckOnUpdateIfStartedAndThenStopedPlaying)
            {
                if (wasPlaying)
                {
                    if (!audioSource.isPlaying)
                        Destroy(gameObject);
                }
                else
                    wasPlaying = audioSource.isPlaying;
            }
        }

        /// <summary>
        /// Set <paramref name="gameObject"/> to be destroyed when <see cref="AudioSource"/> stop playing.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to check.</param>
        /// <param name="mode">How checking will occur.</param>
        public static void AddComponent(GameObject gameObject, Mode mode = Mode.CheckOnStartForClipLength)
            => gameObject.AddComponent<DestroyWhenAudioSourceEnds>().mode = mode;
    }
}
