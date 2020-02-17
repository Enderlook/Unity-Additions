using Enderlook.Unity.Serializables.PolySwitcher;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(ShortPolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + "Short")]
    public class ShortPolySwitchGet : AtomPolySwitchGetter<PolySwitchShort, short> { }
}