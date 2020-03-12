﻿using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchUShort), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchUShort))]
    public class PolySwitchUShort : PolySwitch<ushort> { }
}