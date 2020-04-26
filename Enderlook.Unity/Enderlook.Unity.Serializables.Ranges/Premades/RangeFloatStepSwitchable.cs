using System;

namespace Enderlook.Unity.Serializables.Ranges
{
    [Serializable]
    public class RangeFloatStepSwitchable : Switch<RangeFloatStep, float>, IBasicRange<float>, IBasicRangeInt<float>
    {
        public float Value => Mode == 1 ? Value1.Value : Value2;

        public int ValueInt => Mode == 1 ? Value1.ValueInt : RangeFloat.FloatToIntByChance(Value2);

        /// <summary>
        /// Return the stored value.
        /// </summary>
        /// <param name="source"><see cref="RangeFloatSwitchable"/> instance used to determine the random float.</param>
        public static explicit operator float(RangeFloatStepSwitchable source) => source.Value;

        /// <summary>
        /// Return the stored value.
        /// The result is always an integer. Decimal numbers are used as chance to increment by one.
        /// </summary>
        /// <param name="source"><see cref="RangeFloatSwitchable"/> instance used to determine the random int.</param>
        public static explicit operator int(RangeFloatStepSwitchable source) => source.ValueInt;
    }
}