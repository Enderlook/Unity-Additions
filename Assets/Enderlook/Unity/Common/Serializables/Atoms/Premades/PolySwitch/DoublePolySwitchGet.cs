using Enderlook.Unity.Serializables.PolySwitcher;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(DoublePolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + nameof(Double))]
    public class DoublePolySwitchGet : AtomPolySwitchGetter<PolySwitchDouble, double> { }
}