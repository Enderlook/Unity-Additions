﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.PolySwitcher
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchVector3), menuName = nameof(PolySwitcher) + "/Types/" + nameof(PolySwitchVector3))]
    public class PolySwitchVector3 : PolySwitch<Vector3> { }
}