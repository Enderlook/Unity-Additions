using System;

namespace Enderlook.Unity.Utils.Clockworks
{
    /// <inheritdoc cref="IClockwork"/>
    public class Clockwork : BaseCallbackClockwork<Action>
    {
        /// <summary>
        /// Create a timer that executes <paramref name="callback"/> each <paramref name="cooldown"/> seconds.<br/>
        /// Time must be manually updated using <see cref="BasicClockwork.Recharge(float)"/> or <see cref="TryExecute(float)"/> methods.
        /// </summary>
        /// <param name="cooldown">Time in seconds to execute <paramref name="callback"/>.</param>
        /// <param name="callback">Action to execute.</param>
        /// <param name="autoExecute">Whenever <see cref="BaseCallbackClockwork{T}.UpdateBehaviour(float)"/> must call <see cref="Execute"/> when <see cref="BasicClockwork.CooldownTime"/> is 0.</param>
        /// <param name="cycles">Number of times <see cref="Execute"/> can be call. Use -1 for unlimited. Use <see cref="ResetCycles()"/> to recover their uses. Don't use 0 or the timer will be disabled by default.</param>
        public Clockwork(float cooldown, Action callback, bool autoExecute = true, int cycles = -1) : base(cooldown, callback, autoExecute, cycles) { }

        /// <inheritdoc />
        public sealed override void Execute() => callback();

        /// <inheritdoc />
        public sealed override bool TryExecute(float deltaTime = 0)
        {
            if (MustExecute())
            {
                callback();
                return true;
            }
            return false;
        }
    }
}