using System;

namespace Enderlook.Unity.Serializables.Ranges
{
    [Serializable]
    public class RangeFloatSwitchable : Switch<RangeFloat, float>, IBasicRange<float>, IBasicRangeInt<float>
    {
        public float Value => Mode == 1 ? Value1.Value : Value2;

        public int ValueInt => Mode == 1 ? Value1.ValueInt : RangeFloat.FloatToIntByChance(Value2);

        /// <summary>
        /// Return the stored value.
        /// </summary>
        /// <param name="source"><see cref="RangeFloatSwitchable"/> instance used to determine the random float.</param>
        public static explicit operator float(RangeFloatSwitchable source) => source.Value;

        /// <summary>
        /// Return the stored value.
        /// The result is always an integer. Decimal numbers are used as chance to increment by one.
        /// </summary>
        /// <param name="source"><see cref="RangeFloatSwitchable"/> instance used to determine the random int.</param>
        public static explicit operator int(RangeFloatSwitchable source) => source.ValueInt;
    }
}