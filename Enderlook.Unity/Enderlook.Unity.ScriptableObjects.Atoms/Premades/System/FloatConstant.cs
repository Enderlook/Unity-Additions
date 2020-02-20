using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(FloatConstant), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + "Float")]
    public class FloatConstant : AtomConstant<float> { }
}
