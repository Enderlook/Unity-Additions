﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchULong), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchULong))]
    public class PolySwitchULong : PolySwitch<ulong> { }
}