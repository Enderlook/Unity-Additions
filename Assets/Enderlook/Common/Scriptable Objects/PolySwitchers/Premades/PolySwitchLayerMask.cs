using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchLayerMask), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchLayerMask))]
    public class PolySwitchLayerMask : PolySwitch<LayerMask> { }
}