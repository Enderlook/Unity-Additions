using UnityEngine;

namespace Enderlook.Unity.Utils.Clockworks
{
    /// <summary>
    /// This class represent a cooldown which executes a certain callback with return value on end.<br/>
    /// <see cref="Interfaces.IUpdate.UpdateBehaviour(float)"/> must be executed on every frame with <see cref="Time.deltaTime"/>.
    /// </summary>
    public interface IClockwork<T> : IClockwork
    {
        /// <summary>
        /// Execute the callback and call <see cref="IBasicClockwork.ResetTime()"/>.<br/>
        /// It ignores the <see cref="IBasicClockwork.IsReady"/>. Use <seealso cref="IClockwork.TryExecute(float)"/> to use it.
        /// </summary>
        /// <returns>The result ofthe callback>.</returns>
        /// <seealso cref="TryExecute(out T, float)"/>
        T ExecuteWithReturn();

        /// <summary>
        /// Try to execute the callback. It will check for the <see cref="IBasicClockwork.CooldownTime"/>, and if possible, execute.
        /// </summary>
        /// <param name="result">The result of the callback.</param>
        /// <param name="deltaTime">Time since the last frame. <see cref="Time.deltaTime"/></param>
        /// <returns><see langword="true"/> if it was executed, <see langword="false"/> if it's still on cooldown.</returns>
        /// <seealso cref="ExecuteWithReturn"/>
        bool TryExecute(out T result, float deltaTime = 0);
    }
}