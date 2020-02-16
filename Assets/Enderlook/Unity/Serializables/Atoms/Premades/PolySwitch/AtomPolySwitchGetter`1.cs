﻿using Enderlook.Unity.Serializables.PolySwitcher;

using System;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable]
    public abstract class AtomPolySwitchGetter<U, T> : AtomGetter<U, T> where U : PolySwitch<T>
    {
        public override T Value => value.Value;
    }
}