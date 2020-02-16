﻿using Enderlook.Unity.Serializables.PolySwitcher;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(UIntPolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + "UInt")]
    public class UIntPolySwitchGet : AtomPolySwitchGetter<PolySwitchUInt, uint> { }
}