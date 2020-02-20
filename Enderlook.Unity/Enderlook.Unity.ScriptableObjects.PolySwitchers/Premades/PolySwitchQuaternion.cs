using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchQuaternion), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchQuaternion))]
    public class PolySwitchQuaternion : PolySwitch<Quaternion> { }
}