using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(UShortConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + "UShort")]
    public class UShortConstant : AtomConstant<ushort> { }
}
