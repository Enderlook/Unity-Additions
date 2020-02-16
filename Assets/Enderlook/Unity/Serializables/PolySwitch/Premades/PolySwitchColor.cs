﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchColor), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchColor))]
    public class PolySwitchColor : PolySwitch<Color> { }
}