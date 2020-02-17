using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchVector4), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchVector4))]
    public class PolySwitchVector4 : PolySwitch<Vector4> { }
}