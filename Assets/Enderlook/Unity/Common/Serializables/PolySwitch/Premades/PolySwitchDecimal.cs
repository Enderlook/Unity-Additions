using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchDecimal), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchDecimal))]
    public class PolySwitchDecimal : PolySwitch<decimal> { }
}