using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchDecimal), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchDecimal))]
    public class PolySwitchDecimal : PolySwitch<decimal> { }
}