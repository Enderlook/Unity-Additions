using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(FloatVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + "Float")]
    public class FloatVariable : AtomVariable<float> { }
}
