﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector3), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Resettables/" + nameof(Vector3))]
    public class Vector3VariableResettable : AtomVariableResettable<Vector3> { }
}
