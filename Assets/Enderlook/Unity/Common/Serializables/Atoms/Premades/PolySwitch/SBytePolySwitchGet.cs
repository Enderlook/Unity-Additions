﻿using Enderlook.Unity.Serializables.PolySwitcher;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(SBytePolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + "SByte")]
    public class SBytePolySwitchGet : AtomPolySwitchGetter<PolySwitchSByte, sbyte> { }
}