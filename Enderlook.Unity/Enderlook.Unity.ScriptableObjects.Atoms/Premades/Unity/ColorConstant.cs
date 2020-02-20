using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(ColorConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + nameof(Color))]
    public class ColorConstant : AtomConstant<Color> { }
}
