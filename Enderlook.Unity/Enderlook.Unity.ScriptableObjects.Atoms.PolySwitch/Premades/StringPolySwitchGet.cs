using Enderlook.Unity.ScriptableObjects.PolySwitchers;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(StringPolySwitchGet), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Others/PolySwitch/" + nameof(String))]
    public class StringPolySwitchGet : AtomPolySwitchGetter<PolySwitchString, string> { }
}