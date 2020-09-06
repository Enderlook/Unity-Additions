using System;

namespace Enderlook.Unity.Utils.Clockworks
{
    /// <inheritdoc cref="IClockwork"/>
    public abstract class BaseCallbackClockwork<T> : BasicClockwork, IClockwork where T : Delegate
    {
        /// <summary>
        /// Callback method to execute.
        /// </summary>
        protected T callback;

        private bool autoExecute;

        /// <summary>
        /// Create a timer that executes <paramref name="callback"/> each <paramref name="cooldown"/> seconds.<br/>
        /// Time must be manually updated using <see cref="BasicClockwork.Recharge(float)"/>, <see cref="TryExecute(float)"/> or <see cref="TryExecute(out T, float)"/> methods.
        /// </summary>
        /// <param name="cooldown">Time in seconds to execute <paramref name="callback"/>.</param>
        /// <param name="callback">Action to execute.</param>
        /// <param name="autoExecute">Whenever <see cref="UpdateBehaviour(float)"/> must call <see cref="Execute"/> when <see cref="BasicClockwork.CooldownTime"/> is 0.</param>
        /// <param name="cycles">Number of times <see cref="Execute"/> can be call. Use -1 for unlimited. Use <see cref="ResetCycles()"/> to recover their uses. Don't use 0 or the timer will be disabled by default.</param>
        public BaseCallbackClockwork(float cooldown, T callback, bool autoExecute = true, int cycles = -1) : base(cooldown)
        {
            ResetCycles(cycles);
            this.callback = callback;
            this.autoExecute = autoExecute;
        }

        /// <inheritdoc cref="IClockwork.TotalCycles"/>
        public int TotalCycles { get; set; }

        /// <inheritdoc cref="IClockwork.RemainingCycles"/>
        public int RemainingCycles { get; set; }

        /// <inheritdoc cref="IClockwork.IsEndlessLoop"/>
        public bool IsEndlessLoop => TotalCycles == -1;

        /// <inheritdoc cref="IClockwork.IsEnabled"/>
        public bool IsEnabled => RemainingCycles > 0 || IsEndlessLoop;

        /// <inheritdoc cref="IClockwork.Execute"/>
        public abstract void Execute();

        /// <summary>
        /// This method is used inside <see cref="TryExecute(float)"/>. On <see langword="true"/>, <see cref="callback"/> must be executed.<br/>
        /// You do not need to execute <see cref="BasicClockwork.ResetTime()"/> when you executes this method.
        /// </summary>
        /// <returns>Whenever <see cref="callback"/> must be executed or not.</returns>
        protected bool MustExecute()
        {
            if (ReduceCyclesByOne())
            {
                ResetTime();
                return true;
            }
            return false;
        }

        /// <inheritdoc cref="IClockwork.ResetCycles()"/>
        public void ResetCycles() => RemainingCycles = TotalCycles;

        /// <inheritdoc cref="IClockwork.ResetCycles(int)"/>
        public void ResetCycles(int newCycles)
        {
            TotalCycles = newCycles;
            ResetCycles();
        }

        /// <inheritdoc cref="IClockwork.TryExecute(float)"/>
        public abstract bool TryExecute(float deltaTime = 0);

        private bool ReduceCyclesByOne()
        {
            if (IsEndlessLoop)
                return true;
            bool enabled = IsEnabled;
            RemainingCycles--;
            return enabled;
        }

        /// <summary>
        /// Calls <see cref="BasicClockwork.Recharge(float)"/> if <see cref="IsEnabled"/>, and calls <see cref="Execute"/> if <see cref="autoExecute"/> is <see langword="true"/> and <see cref="BasicClockwork.Recharge(float)"/> returned <see langword="true"/>.
        /// </summary>
        /// <param name="deltaTime">Time since last increase.</param>
        public override void UpdateBehaviour(float deltaTime)
        {
            if (IsEnabled && Recharge(deltaTime) && autoExecute && MustExecute())
                Execute();
        }

        /// <inheritdoc />
        public void SetReady() => CooldownTime = 0;
    }
}