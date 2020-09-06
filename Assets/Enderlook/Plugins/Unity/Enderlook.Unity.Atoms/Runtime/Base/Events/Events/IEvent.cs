namespace Enderlook.Unity.Atoms
{
    public interface IEvent<TValue> : IEventRegister<TValue>
    {
        /// <summary>
        /// Execute event.
        /// </summary>
        /// <param name="parameter">Parameter passed to the event.</param>
        void RaiseEvent(TValue parameter);
    }
}