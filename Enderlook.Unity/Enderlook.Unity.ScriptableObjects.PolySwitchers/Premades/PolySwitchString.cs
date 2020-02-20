using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchString), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchString))]
    public class PolySwitchString : PolySwitch<string> { }
}