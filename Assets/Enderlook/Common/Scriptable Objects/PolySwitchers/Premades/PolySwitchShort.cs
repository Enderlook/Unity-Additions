using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchShort), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchShort))]
    public class PolySwitchShort : PolySwitch<short> { }
}