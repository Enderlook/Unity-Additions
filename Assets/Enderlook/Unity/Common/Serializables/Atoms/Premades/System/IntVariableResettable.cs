using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(IntVariableResettable), menuName = nameof(Atom) + "/Variables/Resettables/" + "Int")]
    public class IntVariableResettable : AtomVariableResettable<int> { }
}
