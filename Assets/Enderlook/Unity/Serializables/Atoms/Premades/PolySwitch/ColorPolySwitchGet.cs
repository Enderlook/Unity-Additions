﻿using Enderlook.Unity.Serializables.PolySwitcher;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(ColorPolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + nameof(Color))]
    public class ColorPolySwitchGet : AtomPolySwitchGetter<PolySwitchColor, Color> { }
}