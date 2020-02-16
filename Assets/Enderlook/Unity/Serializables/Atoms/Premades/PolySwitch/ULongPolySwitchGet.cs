using Enderlook.Unity.Serializables.PolySwitcher;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(ULongPolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + "ULong")]
    public class ULongPolySwitchGet : AtomPolySwitchGetter<PolySwitchULong, ulong> { }
}