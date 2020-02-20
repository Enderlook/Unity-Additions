using Enderlook.Unity.ScriptableObjects.PolySwitchers;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector2PolySwitchGet), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Others/PolySwitch/" + nameof(Vector2))]
    public class Vector2PolySwitchGet : AtomPolySwitchGetter<PolySwitchVector2, Vector2> { }
}