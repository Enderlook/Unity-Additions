using Enderlook.Unity.ScriptableObjects.PolySwitchers;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(QuaternionPolySwitchGet), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Others/PolySwitch/" + nameof(Quaternion))]
    public class QuaternionPolySwitchGet : AtomPolySwitchGetter<PolySwitchQuaternion, Quaternion> { }
}