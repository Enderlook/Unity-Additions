using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(UShortVariableResettable), menuName = nameof(Atom) + "/Variables/Resettables/" + "UShort")]
    public class UShortVariableResettable : AtomVariableResettable<ushort> { }
}
