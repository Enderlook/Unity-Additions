﻿using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchChar), menuName = nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchChar))]
    public class PolySwitchChar : PolySwitch<char> { }
}