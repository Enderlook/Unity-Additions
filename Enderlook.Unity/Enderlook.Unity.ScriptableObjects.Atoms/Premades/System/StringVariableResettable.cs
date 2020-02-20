using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(StringVariableResettable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Resettables/" + nameof(String))]
    public class StringVariableResettable : AtomVariableResettable<string> { }
}
