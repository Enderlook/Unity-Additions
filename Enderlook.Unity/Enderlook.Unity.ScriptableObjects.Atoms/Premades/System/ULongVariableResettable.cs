using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(ULongVariableResettable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Resettables/" + "ULong")]
    public class ULongVariableResettable : AtomVariableResettable<ulong> { }
}
