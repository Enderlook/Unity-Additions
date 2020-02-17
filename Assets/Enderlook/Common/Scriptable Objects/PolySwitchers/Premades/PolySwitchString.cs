using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchString), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchString))]
    public class PolySwitchString : PolySwitch<string> { }
}