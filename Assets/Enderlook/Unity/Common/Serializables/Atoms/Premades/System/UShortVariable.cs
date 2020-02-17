using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = "UShort", menuName = nameof(Atom) + "/Variables/Commons/" + "UShort")]
    public class UShortVariable : AtomVariable<ushort> { }
}
