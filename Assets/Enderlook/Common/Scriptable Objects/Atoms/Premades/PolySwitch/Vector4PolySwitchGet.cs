﻿using Enderlook.Unity.ScriptableObjects.PolySwitchers;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector3PolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + nameof(Vector4))]
    public class Vector4PolySwitchGet : AtomPolySwitchGetter<PolySwitchVector4, Vector4> { }
}