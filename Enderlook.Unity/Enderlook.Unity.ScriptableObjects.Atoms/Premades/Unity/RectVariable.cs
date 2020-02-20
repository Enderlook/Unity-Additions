using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.Unity
{
    [Serializable, CreateAssetMenu(fileName = nameof(RectVariable), menuName = "Enderlook/" + nameof(Atom) + "/Variables/Commons/" + nameof(Rect))]
    public class RectVariable : AtomVariable<Rect> { }
}
