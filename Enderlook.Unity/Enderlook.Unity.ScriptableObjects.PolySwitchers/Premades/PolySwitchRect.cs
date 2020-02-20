using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchRect), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchRect))]
    public class PolySwitchRect : PolySwitch<Rect> { }
}