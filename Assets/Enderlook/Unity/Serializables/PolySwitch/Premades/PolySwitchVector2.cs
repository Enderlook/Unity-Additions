using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchVector2), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchVector2))]
    public class PolySwitchVector2 : PolySwitch<Vector2> { }
}