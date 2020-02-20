using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(ShortVariableResettable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Resettables/" + "Short")]
    public class ShortVariableResettable : AtomVariableResettable<short> { }
}
