using Enderlook.Unity.Serializables.PolySwitcher;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms.Premades.PolySwitch
{
    [Serializable, CreateAssetMenu(fileName = nameof(BytePolySwitchGet), menuName = nameof(Atom) + "/Variables/Others/PolySwitch/" + "Byte")]
    public class BytePolySwitchGet : AtomPolySwitchGetter<PolySwitchByte, byte> { }
}