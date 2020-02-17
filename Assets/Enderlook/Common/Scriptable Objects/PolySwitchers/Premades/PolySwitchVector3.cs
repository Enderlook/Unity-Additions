using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchVector3), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchVector3))]
    public class PolySwitchVector3 : PolySwitch<Vector3> { }
}