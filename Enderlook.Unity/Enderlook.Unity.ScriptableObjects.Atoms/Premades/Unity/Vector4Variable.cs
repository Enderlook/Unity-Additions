using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector4), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + nameof(Vector4))]
    public class Vector4Variable : AtomVariable<Vector4> { }
}
