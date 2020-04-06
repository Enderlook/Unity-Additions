using UnityEngine;

namespace Enderlook.Unity.Utils.Clockworks
{
    /// <summary>
    /// This class represent a cooldown which executes a certain callback on end.<br/>
    /// <see cref="Interfaces.IUpdate.UpdateBehaviour(float)"/> must be executed on every frame with <see cref="Time.deltaTime"/>.
    /// </summary>
    public interface IClockwork : IBasicClockwork
    {
        /// <summary>
        /// Execute the callback and call <see cref="IBasicClockwork.ResetTime()"/>.<br/>
        /// It ignores the <see cref="IBasicClockwork.IsReady"/>. Use <seealso cref="TryExecute(float)"/> to use it.
        /// </summary>
        /// <seealso cref="TryExecute(float)"/>
        void Execute();

        /// <summary>
        /// Try to execute the callback. It will check for the <see cref="IBasicClockwork.CooldownTime"/>, and if possible, execute.
        /// </summary>
        /// <param name="deltaTime">Time since the last frame. <see cref="Time.deltaTime"/></param>
        /// <returns><see langword="true"/> if it was executed, <see langword="false"/> if it's still on cooldown.</returns>
        /// <seealso cref="Execute"/>
        bool TryExecute(float deltaTime = 0);

        /// <summary>
        /// Total number of times <see cref="Execute"/> can be called. -1 is unlimited.
        /// </summary>
        int TotalCycles { get; }

        /// <summary>
        /// Remaining number of times <see cref="Execute"/> can be called.
        /// </summary>
        int RemainingCycles { get; }

        /// <summary>
        /// Whenever there is no number of time <see cref="Execute"/> can be called.
        /// </summary>
        bool IsEndlessLoop { get; }

        /// <summary>
        /// Whenever the timer is working or not. If <see cref="RemainingCycles"/> is 0 the timer stop working.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Reset <see cref="RemainingCycles"/> to <see cref="TotalCycles"/>.
        /// </summary>
        void ResetCycles();

        /// <summary>
        /// Assign a new maximum value <paramref name="newCycles"/> to <see cref="TotalCycles"/> and <see cref="RemainingCycles"/>.
        /// </summary>
        /// <param name="newCycles">New maximum amount of cycles.</param>
        void ResetCycles(int newCycles);

        /// <summary>
        /// Set clockwork ready to be used by setting <see cref="IBasicClockwork.CooldownTime"/> to 0 and <see cref="IBasicClockwork.WarmupTime"/> to <see cref="IBasicClockwork.TimeLength"/>.
        /// </summary>
        void SetReady();
    }
}