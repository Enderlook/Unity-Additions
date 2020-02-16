﻿using Enderlook.Unity.Serializables.PolySwitcher;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(IntPolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + "Int")]
    public class IntPolySwitchGet : AtomPolySwitchGetter<PolySwitchInt, int> { }
}