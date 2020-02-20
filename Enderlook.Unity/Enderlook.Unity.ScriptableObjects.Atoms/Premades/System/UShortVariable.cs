using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = "UShort", menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + "UShort")]
    public class UShortVariable : AtomVariable<ushort> { }
}
