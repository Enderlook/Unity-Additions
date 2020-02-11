using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [CreateAssetMenu(fileName = nameof(PolySwitchFloat), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchFloat))]
    public class PolySwitchFloat : PolySwitch<float> { }
}