using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchQuaternion), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchQuaternion))]
    public class PolySwitchQuaternion : PolySwitch<Quaternion> { }
}