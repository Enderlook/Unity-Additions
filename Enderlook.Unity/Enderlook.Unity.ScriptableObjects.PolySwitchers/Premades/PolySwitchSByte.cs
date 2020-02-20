using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchSByte), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchSByte))]
    public class PolySwitchSByte : PolySwitch<sbyte> { }
}