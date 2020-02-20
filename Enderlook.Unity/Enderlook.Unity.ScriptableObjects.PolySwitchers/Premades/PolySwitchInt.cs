using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchInt), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchInt))]
    public class PolySwitchInt : PolySwitch<int> { }
}