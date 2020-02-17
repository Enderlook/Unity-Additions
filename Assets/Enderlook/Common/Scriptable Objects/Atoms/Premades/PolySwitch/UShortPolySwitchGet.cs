using Enderlook.Unity.ScriptableObjects.PolySwitchers;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(UShortPolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + "UShort")]
    public class UShortPolySwitchGet : AtomPolySwitchGetter<PolySwitchUShort, ushort> { }
}