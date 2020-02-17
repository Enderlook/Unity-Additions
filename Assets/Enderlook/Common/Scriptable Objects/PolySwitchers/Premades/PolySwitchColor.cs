using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchColor), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchColor))]
    public class PolySwitchColor : PolySwitch<Color> { }
}