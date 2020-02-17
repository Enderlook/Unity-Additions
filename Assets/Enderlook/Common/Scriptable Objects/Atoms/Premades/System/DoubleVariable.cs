using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(DoubleVariable), menuName = nameof(Atom) + "/Variables/Commons/" + nameof(Double))]
    public class DoubleVariable : AtomVariable<double> { }
}
