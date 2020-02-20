using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(UIntConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + "UInt")]
    public class UIntConstant : AtomConstant<uint> { }
}
