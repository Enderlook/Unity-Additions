﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(DecimalVariableResettable), menuName = nameof(Atom) + "/Variables/Resettables/" + nameof(Decimal))]
    public class DecimalVariableResettable : AtomVariableResettable<decimal> { }
}
