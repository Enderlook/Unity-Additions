using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchLayerMask), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchLayerMask))]
    public class PolySwitchLayerMask : PolySwitch<LayerMask> { }
}