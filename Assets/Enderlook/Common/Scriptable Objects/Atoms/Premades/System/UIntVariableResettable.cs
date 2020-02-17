using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(UIntVariableResettable), menuName = nameof(Atom) + "/Variables/Resettables/" + "UInt")]
    public class UIntVariableResettable : AtomVariableResettable<uint> { }
}
