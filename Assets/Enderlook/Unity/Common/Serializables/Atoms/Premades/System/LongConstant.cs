using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(LongVariableResettable), menuName = nameof(Atom) + "/Variables/Constants/" + "Long")]
    public class LongConstant : AtomConstant<long> { }
}
