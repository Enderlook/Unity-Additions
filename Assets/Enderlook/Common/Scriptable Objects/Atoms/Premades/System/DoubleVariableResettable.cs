﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(DoubleVariableResettable), menuName = nameof(Atom) + "/Variables/Resettables/" + nameof(Double))]
    public class DoubleVariableResettable : AtomVariableResettable<double> { }
}
