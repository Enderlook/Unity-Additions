namespace Enderlook.Unity.Atoms
{
    public interface IEvent2<TValue> : IEventRegister2<TValue>
    {
        /// <summary>
        /// Execute event.
        /// </summary>
        /// <param name="parameter1">First parameter passed to the event.</param>
        /// <param name="parameter2">Second parameter passed to the event.</param>
        void RaiseEvent(TValue parameter1, TValue parameter2);
    }
}