using Enderlook.Unity.ScriptableObjects.PolySwitchers;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(RectPolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + nameof(Rect))]
    public class RectPolySwitchGet : AtomPolySwitchGetter<PolySwitchRect, Rect> { }
}