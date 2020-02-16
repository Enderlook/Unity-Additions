using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchQuaternion), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchQuaternion))]
    public class PolySwitchQuaternion : PolySwitch<Quaternion> { }
}