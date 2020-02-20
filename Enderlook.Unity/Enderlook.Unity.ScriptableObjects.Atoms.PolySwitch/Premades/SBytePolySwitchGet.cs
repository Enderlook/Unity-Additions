using Enderlook.Unity.ScriptableObjects.PolySwitchers;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(SBytePolySwitchGet), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Others/PolySwitch/" + "SByte")]
    public class SBytePolySwitchGet : AtomPolySwitchGetter<PolySwitchSByte, sbyte> { }
}