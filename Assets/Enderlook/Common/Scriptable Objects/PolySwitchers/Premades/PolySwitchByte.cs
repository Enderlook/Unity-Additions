using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchByte), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchByte))]
    public class PolySwitchByte : PolySwitch<byte> { }
}