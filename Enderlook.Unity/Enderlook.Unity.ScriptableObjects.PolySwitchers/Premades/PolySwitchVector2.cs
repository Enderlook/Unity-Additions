using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchVector2), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchVector2))]
    public class PolySwitchVector2 : PolySwitch<Vector2> { }
}