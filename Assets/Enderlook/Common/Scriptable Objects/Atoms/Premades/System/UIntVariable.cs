using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(UIntVariable), menuName = nameof(Atom) + "/Variables/Commons/" + "UInt")]
    public class UIntVariable : AtomVariable<uint> { }
}
