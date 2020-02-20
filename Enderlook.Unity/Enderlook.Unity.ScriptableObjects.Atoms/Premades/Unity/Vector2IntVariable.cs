using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector2Int), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + nameof(Vector2Int))]
    public class Vector2IntVariable : AtomVariable<Vector2Int> { }
}
