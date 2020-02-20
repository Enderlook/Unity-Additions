using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(ShortVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + "Short")]
    public class ShortVariable : AtomVariable<short> { }
}
