using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(SByteConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + "SByte")]
    public class SByteConstant : AtomConstant<sbyte> { }
}
