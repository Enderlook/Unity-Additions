using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchUShort), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchUShort))]
    public class PolySwitchUShort : PolySwitch<ushort> { }
}