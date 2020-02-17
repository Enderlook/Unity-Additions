using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.System
{
    [Serializable, CreateAssetMenu(fileName = nameof(CharVariable), menuName = nameof(Atom) + "/Variables/Commons/" + nameof(Char))]
    public class CharVariable : AtomVariable<char> { }
}
