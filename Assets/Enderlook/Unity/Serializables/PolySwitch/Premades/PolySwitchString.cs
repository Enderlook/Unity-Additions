using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchString), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchString))]
    public class PolySwitchString : PolySwitch<string> { }
}