using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchVector4), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchVector4))]
    public class PolySwitchVector4 : PolySwitch<Vector4> { }
}