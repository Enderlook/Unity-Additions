using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchByte), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchByte))]
    public class PolySwitchByte : PolySwitch<byte> { }
}