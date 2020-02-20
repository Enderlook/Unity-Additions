using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(QuaternionVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + nameof(Quaternion))]
    public class QuaternionVariable : AtomVariable<Quaternion> { }
}
