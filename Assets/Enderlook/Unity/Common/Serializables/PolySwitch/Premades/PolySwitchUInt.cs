using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchUInt), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchUInt))]
    public class PolySwitchUInt : PolySwitch<uint> { }
}