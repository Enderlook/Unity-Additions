using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchVector2Int), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchVector2Int))]
    public class PolySwitchVector2Int : PolySwitch<Vector2Int> { }
}