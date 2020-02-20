using Enderlook.Unity.ScriptableObjects.PolySwitchers;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(IntPolySwitchGet), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Others/PolySwitch/" + "Int")]
    public class IntPolySwitchGet : AtomPolySwitchGetter<PolySwitchInt, int> { }
}