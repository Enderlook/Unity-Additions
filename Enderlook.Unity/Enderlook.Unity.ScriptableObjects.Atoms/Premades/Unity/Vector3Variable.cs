using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector3), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + nameof(Vector3))]
    public class Vector3Variable : AtomVariable<Vector3> { }
}
