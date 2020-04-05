using Enderlook.Unity.Utils.Interfaces;

using UnityEngine;

namespace Enderlook.Unity.Utils.Clockworks
{
    public interface IBasicClockwork : IUpdate
    {
        /// <summary>
        /// Time in seconds to finish the countdown.
        /// </summary>
        float CooldownTime { get; }

        /// <summary>
        /// Time in seconds since the countdown started.
        /// </summary>
        float WarmupTime { get; }

        /// <summary>
        /// Total cooldown time.
        /// </summary>
        float TimeLength { get; }

        /// <summary>
        /// Cooldown percent from 1 to 0. When 0, it's ready to execute.
        /// </summary>
        float CooldownPercent { get; }

        /// <summary>
        /// Warmup percent from 0 to 1. When 1, it's ready to execute.
        /// </summary>
        float WarmupPercent { get; }

        /// <summary>
        /// Whenever it's ready or is still in cooldown.
        /// </summary>
        bool IsReady { get; }

        /// <summary>
        /// Reset <see cref="CooldownTime"/> time to <see cref="TimeLength"/> and <see cref="WarmupTime"/> to 0.
        /// </summary>
        void ResetTime();

        /// <summary>
        /// Assign a new maximum value <paramref name="newTimeLength"/> to <see cref="TimeLength"/> and calls <see cref="ResetTime()"/>.
        /// </summary>
        void ResetTime(float newTimeLength);

        /// <summary>
        /// Reduce <see cref="CooldownTime"/> time as increases <see cref="WarmupTime"/> and checks if it's ready.
        /// </summary>
        /// <param name="deltaTime"><see cref="Time.deltaTime"/></param>
        /// <returns><see cref="IsReady"/>.</returns>
        bool Recharge(float deltaTime);

        /// <summary>
        /// Set clockwork ready to be used by setting <see cref="CooldownTime"/> to 0 and <see cref="WarmupTime"/> to <see cref="TimeLength"/>.
        /// </summary>
        void SetReady();
    }
}