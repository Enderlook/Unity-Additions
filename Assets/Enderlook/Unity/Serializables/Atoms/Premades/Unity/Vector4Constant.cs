using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector4), menuName = nameof(Atom) + "/Variables/Constants/" + nameof(Vector4))]
    public class Vector4Constant : AtomConstant<Vector4> { }
}
