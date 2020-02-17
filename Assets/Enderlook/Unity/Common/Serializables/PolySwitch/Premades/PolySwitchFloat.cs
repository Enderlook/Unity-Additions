using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchFloat), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchFloat))]
    public class PolySwitchFloat : PolySwitch<float> { }
}