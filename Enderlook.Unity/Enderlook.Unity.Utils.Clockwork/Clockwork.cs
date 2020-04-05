using System;

using UnityEngine;

namespace Enderlook.Unity.Utils.Clockworks
{
    public class Clockwork : IClockwork
    {
        private Action Callback;

        private bool autoExecute;

        /// <inheritdoc />
        public float CooldownTime {
            get => cooldownTime;
            private set {
                cooldownTime = value;
                if (cooldownTime < 0)
                    cooldownTime = 0;
            }
        }

        /// <summary>
        /// Time in seconds to execute <see cref="Callback"/>.
        /// </summary>
        protected float cooldownTime = 0f;

        /// <inheritdoc />
        public float TimeLength { get; protected set; }

        /// <inheritdoc />
        public float CooldownPercent => Mathf.Clamp01(CooldownTime / TimeLength);

        /// <inheritdoc />
        public float WarmupTime => TimeLength - cooldownTime;

        /// <inheritdoc />
        public float WarmupPercent => Mathf.Clamp01(WarmupTime / TimeLength);

        /// <inheritdoc />
        public bool IsReady => CooldownTime <= 0;

        /// <inheritdoc />
        public int TotalCycles { get; private set; }

        /// <inheritdoc />
        public int RemainingCycles { get; private set; }

        /// <inheritdoc />
        public bool IsEndlessLoop => TotalCycles == -1;

        /// <inheritdoc />
        public bool IsEnabled => RemainingCycles > 0 || IsEndlessLoop;

        /// <summary>
        /// Create a timer that executes <paramref name="Callback"/> each <paramref name="cooldown"/> seconds.<br/>
        /// Time must be manually updated using <see cref="Recharge(float)"/>, <see cref="TryExecute(float)"/> or <see cref="TryExecute(out T, float)"/> methods.
        /// </summary>
        /// <param name="cooldown">Time in seconds to execute <paramref name="Callback"/>.</param>
        /// <param name="Callback">Action to execute.</param>
        /// <param name="autoExecute">Whenever <see cref="UpdateBehaviour(float)"/> must call <see cref="Execute"/> when <see cref="CooldownTime"/> is 0.</param>
        /// <param name="cycles">Number of times <see cref="Execute"/> can be call. Use -1 for unlimited. Use <see cref="ResetCycles"/> to recover their uses. Don't use 0 or the timer will be disabled by default.</param>
        public Clockwork(float cooldown, Action Callback, bool autoExecute = true, int cycles = -1)
        {
            ResetCycles(cycles);
            ResetTime(cooldown);
            this.Callback = Callback;
            this.autoExecute = autoExecute;
        }

        /// <inheritdoc />
        public void Execute()
        {
            if (ReduceCyclesByOne())
            {
                ResetTime();
                Callback();
            }
        }

        /// <inheritdoc />
        private bool ReduceCyclesByOne()
        {
            if (IsEndlessLoop)
                return true;
            bool enabled = IsEnabled;
            RemainingCycles--;
            return enabled;
        }

        /// <inheritdoc />
        public bool TryExecute(float deltaTime = 0)
        {
            if (IsEnabled && Recharge(deltaTime))
            {
                Execute();
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public void ResetTime() => CooldownTime = TimeLength;

        /// <inheritdoc />
        public void ResetTime(float newCooldownTime)
        {
            TimeLength = newCooldownTime;
            ResetTime();
        }

        /// <inheritdoc />
        public void ResetCycles() => RemainingCycles = TotalCycles;

        /// <inheritdoc />
        public void ResetCycles(int newCycles)
        {
            TotalCycles = newCycles;
            ResetCycles();
        }

        /// <inheritdoc />
        public bool Recharge(float deltaTime)
        {
            if (IsEnabled)
            {
                CooldownTime -= deltaTime;
                return IsReady;
            }
            return false;
        }

        /// <summary>
        /// Calls <see cref="Recharge(float)"/> if <see cref="IsEnabled"/>, and calls <see cref="Execute"/> if <see cref="autoExecute"/> is <see langword="true"/> and <see cref="Recharge(float)"/> returned <see langword="true"/>.
        /// </summary>
        /// <param name="deltaTime">Time since last increase.</param>
        public void UpdateBehaviour(float deltaTime)
        {
            if (IsEnabled && Recharge(deltaTime) && autoExecute)
                Execute();
        }

        /// <inheritdoc />
        public void SetReady() => CooldownTime = 0;
    }
}