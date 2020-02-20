using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(CharConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + nameof(Char))]
    public class CharConstant : AtomConstant<char> { }
}
