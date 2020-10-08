using Enderlook.Unity.Attributes;

using System;

using UnityEngine;

namespace Enderlook.Unity.Components.ScriptableSound
{
    [Serializable]
    public struct PlayEvent
    {
#pragma warning disable CS0649
        [SerializeField, Tooltip("Whenever it should play on this event.")]
        private bool play;

        [SerializeField, Tooltip("Which playlist index play when the event is raised."), ShowIf(nameof(play)), Indented]
        private int indexToPlay;
#pragma warning restore CS0649

        internal bool ShouldPlay(out int index)
        {
            index = indexToPlay;
            return play;
        }

        internal void Validate(int maxIndex) => indexToPlay = Mathf.Max(Mathf.Min(indexToPlay, maxIndex), 0);
    }
}