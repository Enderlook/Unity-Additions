using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector3Int), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + nameof(Vector3Int))]
    public class Vector3IntVariable : AtomVariable<Vector3Int> { }
}
