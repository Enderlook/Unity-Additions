using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(ByteConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + "Byte")]
    public class ByteConstant : AtomConstant<byte> { }
}
