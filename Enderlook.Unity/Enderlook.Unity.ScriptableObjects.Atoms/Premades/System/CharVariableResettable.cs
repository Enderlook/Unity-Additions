using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(CharVariableResettable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Resettables/" + nameof(Char))]
    public class CharVariableResettable : AtomVariableResettable<char> { }
}
