using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(SByteVariableResettable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Resettables/" + "SByte")]
    public class SByteVariableResettable : AtomVariableResettable<sbyte> { }
}
