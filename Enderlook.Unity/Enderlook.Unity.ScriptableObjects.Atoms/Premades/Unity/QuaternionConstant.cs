﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(QuaternionConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + nameof(Quaternion))]
    public class QuaternionConstant : AtomConstant<Quaternion> { }
}