using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(IntConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + "Int")]
    public class IntConstant : AtomConstant<int> { }
}
