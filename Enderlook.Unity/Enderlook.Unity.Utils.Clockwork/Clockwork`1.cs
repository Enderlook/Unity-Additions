using System;

namespace Enderlook.Unity.Utils.Clockworks
{
    /// <inheritdoc cref="IClockwork{T}"/>
    public class Clockwork<T> : BaseCallbackClockwork<Func<T>>, IClockwork<T>
    {
        /// <summary>
        /// Create a timer that executes <paramref name="callback"/> each <paramref name="cooldown"/> seconds.<br/>
        /// Time must be manually updated using <see cref="BasicClockwork.Recharge(float)"/> or <see cref="TryExecute(float)"/> methods.
        /// </summary>
        /// <param name="cooldown">Time in seconds to execute <paramref name="callback"/>.</param>
        /// <param name="callback">Action to execute.</param>
        /// <param name="autoExecute">Whenever <see cref="BaseCallbackClockwork{T}.UpdateBehaviour(float)"/> must call <see cref="Execute"/> when <see cref="BasicClockwork.CooldownTime"/> is 0.</param>
        /// <param name="cycles">Number of times <see cref="Execute"/> can be call. Use -1 for unlimited. Use <see cref="ResetCycles()"/> to recover their uses. Don't use 0 or the timer will be disabled by default.</param>
        public Clockwork(float cooldown, Func<T> callback, bool autoExecute = true, int cycles = -1) : base(cooldown, callback, autoExecute, cycles) { }

        /// <inheritdoc cref="BaseCallbackClockwork{T}.Execute" />
        /// <returns>Value returned from the callback.</returns>
        public T ExecuteWithReturn() => callback();

        /// <inheritdoc cref="BaseCallbackClockwork{T}.Execute" />
        public sealed override void Execute() => callback();

        /// <inheritdoc cref="BaseCallbackClockwork{T}.TryExecute(float)" />
        public sealed override bool TryExecute(float deltaTime = 0)
        {
            if (MustExecute())
            {
                callback();
                return true;
            }
            return false;
        }

        /// <inheritdoc cref="BaseCallbackClockwork{T}.TryExecute(float)"/>
        /// <param name="result">Value returned from the callback. Only read when this method returns <see langword="true"/>.</param>
        public bool TryExecute(out T result, float deltaTime = 0)
        {
            if (MustExecute())
            {
                result = callback();
                return true;
            }
            result = default;
            return false;
        }
    }
}