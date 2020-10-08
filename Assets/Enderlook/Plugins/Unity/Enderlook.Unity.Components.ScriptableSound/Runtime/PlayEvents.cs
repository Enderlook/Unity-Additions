using System;

using UnityEngine;

namespace Enderlook.Unity.Components.ScriptableSound
{
    [Serializable]
    public struct PlayEvents
    {
        [SerializeField]
        private PlayEvent onAwake;

        [SerializeField]
        private PlayEvent onStart;

        [SerializeField]
        private PlayEvent onEnable;

        internal void Validate(int maxIndex)
        {
            onAwake.Validate(maxIndex);
            onStart.Validate(maxIndex);
            onEnable.Validate(maxIndex);
        }

        internal bool OnAwake(out int index) => onAwake.ShouldPlay(out index);

        internal bool OnStart(out int index) => onStart.ShouldPlay(out index);

        internal bool OnEnable(out int index) => onEnable.ShouldPlay(out index);
    }
}