using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchChar), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchChar))]
    public class PolySwitchChar : PolySwitch<char> { }
}