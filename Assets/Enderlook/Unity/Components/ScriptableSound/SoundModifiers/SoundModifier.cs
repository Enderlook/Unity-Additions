﻿using Enderlook.Unity.Attributes;
using Enderlook.Unity.Utils;

using System;

using UnityEngine;

namespace Enderlook.Unity.Components.ScriptableSound.Modifiers
{
    [Serializable, AbstractScriptableObject]
    public class SoundModifier : ScriptableObject, IPrototypable<SoundModifier>
    {
        public virtual void ModifyAudioSource(AudioSource audioSource) => throw new NotImplementedException("This class is 'abstract' and should not be instantiated by its own. Use derived classes instead which override this method.");

        public virtual void BackToNormalAudioSource(AudioSource audioSource) => throw new NotImplementedException("This class is 'abstract' and should not be instantiated by its own. Use derived classes instead which override this method.");

        public virtual SoundModifier CreatePrototype() => throw new NotImplementedException("This class is 'abstract' and should not be instantiated by its own. Use derived classes instead which override this method.");

#if UNITY_EDITOR
        public virtual void Validate(SoundClip soundClip) { }
#endif
    }
}