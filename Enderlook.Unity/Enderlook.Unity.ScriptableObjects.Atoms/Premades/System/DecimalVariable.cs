using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(DecimalVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + nameof(Decimal))]
    public class DecimalVariable : AtomVariable<decimal> { }
}
