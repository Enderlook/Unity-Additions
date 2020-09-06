using System;

namespace Enderlook.Unity.Serializables.Ranges
{
    [Serializable]
    public class RangeIntStepSwitchable : Switch<RangeIntStep, int>, IBasicRangeInt<int>
    {
        public int Value => Mode == 1 ? Value1.Value : Value2;

        public int ValueInt => Mode == 1 ? Value1.Value : Value2;

        /// <summary>
        /// Return the stored value.
        /// </summary>
        /// <param name="source"><see cref="RangeIntStepSwitchable"/> instance used to determine the random int.</param>
        public static explicit operator int(RangeIntStepSwitchable source) => source.Value;
    }
}