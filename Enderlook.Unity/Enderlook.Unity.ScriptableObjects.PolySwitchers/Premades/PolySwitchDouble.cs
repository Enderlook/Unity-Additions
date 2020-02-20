using System;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CreateAssetMenu(fileName = nameof(PolySwitchDouble), menuName = "Enderlook/" + nameof(PolySwitchers) + "/Types/" + nameof(PolySwitchDouble))]
    public class PolySwitchDouble : PolySwitch<double> { }
}