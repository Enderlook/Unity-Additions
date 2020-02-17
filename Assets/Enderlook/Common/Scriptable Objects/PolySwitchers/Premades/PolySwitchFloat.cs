using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchFloat), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchFloat))]
    public class PolySwitchFloat : PolySwitch<float> { }
}