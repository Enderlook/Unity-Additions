using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(LongVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + "Long")]
    public class LongVariable : AtomVariable<long> { }
}
