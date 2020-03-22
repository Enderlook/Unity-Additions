using System;

namespace Enderlook.Unity.Atoms
{
    public interface IEventRegister2<TValue>
    {
        /// <summary>
        /// Register an action to be called when <see cref="OnEvent"/> triggers.
        /// </summary>
        /// <param name="action">Action to register.</param>
        void Register(Action<TValue, TValue> action);

        /// <summary>
        /// Unregister an action to be called when <see cref="OnEvent"/> triggers.
        /// </summary>
        /// <param name="action">Action to unregister.</param>
        void Unregister(Action<TValue, TValue> action);

        /// <summary>
        /// Register an listener to be called when <see cref="OnEvent"/> triggers.
        /// </summary>
        /// <param name="listener">Listener to register.</param>
        void RegisterListener(IEventListener2<TValue> listener);

        /// <summary>
        /// Unregister an listener to be called when <see cref="OnEvent"/> triggers.
        /// </summary>
        /// <param name="listener">Listener to unregister.</param>
        void UnregisterListener(IEventListener2<TValue> listener);
    }
}