﻿using Enderlook.Unity.Serializables.PolySwitcher;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector3IntPolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + nameof(Vector3Int))]
    public class Vector3IntPolySwitchGet : AtomPolySwitchGetter<PolySwitchVector3Int, Vector3Int> { }
}