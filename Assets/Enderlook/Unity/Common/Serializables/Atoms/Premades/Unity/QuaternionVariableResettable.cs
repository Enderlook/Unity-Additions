﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(QuaternionVariableResettable), menuName = nameof(Atom) + "/Variables/Resettables/" + nameof(Quaternion))]
    public class QuaternionVariableResettable : AtomVariableResettable<Quaternion> { }
}