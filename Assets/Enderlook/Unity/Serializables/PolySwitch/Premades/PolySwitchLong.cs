using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchLong), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchLong))]
    public class PolySwitchLong : PolySwitch<long> { }
}