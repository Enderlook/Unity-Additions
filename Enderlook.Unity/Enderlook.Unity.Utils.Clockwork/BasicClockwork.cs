using UnityEngine;

namespace Enderlook.Unity.Utils.Clockworks
{
    public class BasicClockwork : IBasicClockwork
    {
        /// <inheritdoc />
        public float CooldownTime {
            get => cooldownTime;
            private set {
                cooldownTime = value;
                if (cooldownTime < 0)
                    cooldownTime = 0;
            }
        }

        /// <inheritdoc />
        protected float cooldownTime;

        /// <inheritdoc />
        public float WarmupTime => TimeLength - cooldownTime;

        /// <inheritdoc />
        public float WarmupPercent => Mathf.Clamp01(WarmupTime / TimeLength);

        /// <inheritdoc />
        public float TimeLength { get; protected set; }

        /// <inheritdoc />
        public float CooldownPercent => Mathf.Clamp01(CooldownTime / TimeLength);

        /// <inheritdoc />
        public bool IsReady => CooldownTime <= 0;

        /// <summary>
        /// Create a timer.<br/>
        /// Time must be manually updated using <see cref="Recharge(float)"/>.
        /// </summary>
        /// <param name="cooldown">Time in seconds to finish.</param>
        public BasicClockwork(float cooldown)
        {
            TimeLength = cooldown;
            ResetTime();
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
        public bool Recharge(float deltaTime)
        {
            CooldownTime -= deltaTime;
            return IsReady;
        }

        /// <inheritdoc />
        public void SetReady() => CooldownTime = 0;

        /// <summary>
        /// Calls <see cref="Recharge(float)"/>.</summary>
        /// <param name="deltaTime">Time since last increase.</param>
        public void UpdateBehaviour(float deltaTime) => Recharge(deltaTime);
    }
}