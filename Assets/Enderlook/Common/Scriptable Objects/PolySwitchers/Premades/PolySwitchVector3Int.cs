using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchVector3Int), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchVector3Int))]
    public class PolySwitchVector3Int : PolySwitch<Vector3Int> { }
}