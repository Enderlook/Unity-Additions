using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(LongVariable), menuName = nameof(Atom) + "/Variables/Commons/" + "Long")]
    public class LongVariable : AtomVariable<long> { }
}
