using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [CreateAssetMenu(fileName = nameof(PolySwitchShort), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchShort))]
    public class PolySwitchShort : PolySwitch<short> { }
}