using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchUInt), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchUInt))]
    public class PolySwitchUInt : PolySwitch<uint> { }
}