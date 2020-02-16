﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(DecimalConstant), menuName = nameof(Atom) + "/Variables/Constants/" + nameof(Decimal))]
    public class DecimalConstant : AtomConstant<decimal> { }
}
