﻿using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchULong), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchULong))]
    public class PolySwitchULong : PolySwitch<ulong> { }
}