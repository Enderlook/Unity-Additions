﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(ByteVariable), menuName = nameof(Atom) + "/Variables/Commons/" + "Byte")]
    public class ByteVariable : AtomVariable<byte> { }
}
