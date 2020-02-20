using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(ULongVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + "ULong")]
    public class ULongVariable : AtomVariable<ulong> { }
}
