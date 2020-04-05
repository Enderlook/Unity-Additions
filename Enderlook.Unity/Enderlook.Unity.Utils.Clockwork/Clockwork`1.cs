using System;

namespace Enderlook.Unity.Utils.Clockworks
{
    public class Clockwork<T> : Clockwork, IClockwork<T>
    {
        private Func<T> Callback;

        /// <summary>
        /// Create a timer that executes <paramref name="Callback"/> each <paramref name="cooldown"/> seconds.<br/>
        /// Time must be manually updated using <see cref="Clockwork.Recharge(float)"/>, <see cref="Clockwork.TryExecute(float)"/> or <see cref="TryExecute(ref T, float)"/> methods.
        /// </summary>
        /// <param name="cooldown">Time in seconds to execute <paramref name="Callback"/>.</param>
        /// <param name="Callback">Action to execute.</param>
        /// <param name="autoExecute">Whenever <see cref="Clockwork.UpdateBehaviour(float)"/> must call <see cref="Execute"/> when <see cref="Clockwork.CooldownTime"/> is 0.</param>
        public Clockwork(float cooldown, Func<T> Callback, bool autoExecute) : base(cooldown, () => Callback(), autoExecute)
        {
            TimeLength = cooldown;
            ResetTime();
            this.Callback = Callback;
        }

        /// <inheritdoc cref="IClockwork{T}.TryExecute(out T, float)"/>
        public bool TryExecute(out T result, float deltaTime = 0)
        {
            if (Recharge(deltaTime))
            {
                result = Execute();
                return true;
            }
            result = default;
            return false;
        }

        /// <inheritdoc cref="IClockwork{T}.Execute"/>
        public new T Execute()
        {
            ResetTime();
            return Callback();
        }
    }
}