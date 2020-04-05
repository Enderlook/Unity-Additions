using UnityEngine;

namespace Enderlook.Unity.Utils.Clockworks
{
    public interface IClockwork<T> : IClockwork
    {
        /// <summary>
        /// Execute the callback and call <see cref="IBasicClockwork.ResetTime()"/>.<br/>
        /// It ignores the <see cref="IBasicClockwork.IsReady"/>. Use <seealso cref="IClockwork.TryExecute(float)"/> to use it.
        /// </summary>
        /// <returns>The result ofthe callback>.</returns>
        /// <seealso cref="TryExecute(out T, float)"/>
        new T Execute();

        /// <summary>
        /// Try to execute the callback. It will check for the <see cref="IBasicClockwork.CooldownTime"/>, and if possible, execute.
        /// </summary>
        /// <param name="deltaTime">Time since the last frame. <see cref="Time.deltaTime"/></param>
        /// <param name="result">The result of the callback.</param>
        /// <returns><see langword="true"/> if it was executed, <see langword="false"/> if it's still on cooldown.</returns>
        /// <seealso cref="Execute"/>
        bool TryExecute(out T result, float deltaTime = 0);
    }
}