using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchLong), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchLong))]
    public class PolySwitchLong : PolySwitch<long> { }
}