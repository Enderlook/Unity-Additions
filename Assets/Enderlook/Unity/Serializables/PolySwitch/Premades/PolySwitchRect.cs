﻿using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [CreateAssetMenu(fileName = nameof(PolySwitchRect), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchRect))]
    public class PolySwitchRect : PolySwitch<Rect> { }
}