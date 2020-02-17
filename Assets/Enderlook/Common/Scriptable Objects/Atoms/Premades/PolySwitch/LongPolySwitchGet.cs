using Enderlook.Unity.ScriptableObjects.PolySwitchers;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(LongPolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + "Long")]
    public class LongPolySwitchGet : AtomPolySwitchGetter<PolySwitchLong, long> { }
}