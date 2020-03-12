﻿using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(ColorVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + nameof(Color))]
    public class ColorVariable : AtomVariable<Color> { }
}