using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchSByte), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchSByte))]
    public class PolySwitchSByte : PolySwitch<sbyte> { }
}