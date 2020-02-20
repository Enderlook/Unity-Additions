using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(ShortConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + "Short")]
    public class ShortConstant : AtomConstant<short> { }
}
