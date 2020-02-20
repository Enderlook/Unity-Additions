using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector3Int), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Constants/" + nameof(Vector3Int))]
    public class Vector3IntConstant : AtomConstant<Vector3Int> { }
}
