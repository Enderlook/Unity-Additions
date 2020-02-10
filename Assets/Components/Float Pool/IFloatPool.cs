using Additions.Utils;

namespace Additions.Components.FloatPool
{
    public interface IFloatPool : IUpdate
    {
        /// <summary>
        /// Maximum amount. <see cref="Current"/> can't be greater than this value.<br/>
        /// </summary>
        /// <seealso cref="Current"/>
        float Max { get; }

        /// <summary>
        /// Current amount. It can't be greater than <see cref="MaxCurrent"/><br/>
        /// </summary>
        /// <seealso cref="Max"/>
        float Current { get; }

        /// <summary>
        /// Ration between <see cref="Current"/> and <see cref="Max"/>.
        /// </summary>
        float Ratio { get; }

        void Initialize();

        /// <summary>
        /// Reduce <see cref="Current"/> by <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">Amount to reduce <see cref="Current"/>.</param>
        /// <param name="allowUnderflow">Whenever <see cref="Current"/> could reach negative values or not.</param>
        /// <returns><c>remaining</c>: Amount clamped below 0. <c>taken</c>: difference between <paramref name="amount"/> and <c>remaining</c>.</returns>
        (float remaining, float taken) Decrease(float amount, bool allowUnderflow = false);

        /// <summary>
        /// Increase <see cref="Current"/> by <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">Amount to increase <see cref="Current"/>.</param>
        /// <param name="allowOverflow">Whenever <see cref="Current"/> could be higher than <see cref="Max"/> or not.</param>
        /// <returns><c>remaining</c>: Amount clamped above <see cref="Max"/>. <c>taken</c>: difference between <paramref name="amount"/> and <c>remaining</c>.</returns>
        (float remaining, float taken) Increase(float amount, bool allowOverflow = false);
    }
}