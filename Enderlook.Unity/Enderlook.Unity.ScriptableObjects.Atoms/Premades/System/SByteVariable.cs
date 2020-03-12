﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(SByteVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + "SByte")]
    public class SByteVariable : AtomVariable<sbyte> { }
}