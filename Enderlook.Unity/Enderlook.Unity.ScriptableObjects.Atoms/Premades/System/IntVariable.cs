using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(IntVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + "Int")]
    public class IntVariable : AtomVariable<int> { }
}
