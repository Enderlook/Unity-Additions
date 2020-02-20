using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(Vector2), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Resettables/" + nameof(Vector2))]
    public class Vector2VariableResettable : AtomVariableResettable<Vector2> { }
}
