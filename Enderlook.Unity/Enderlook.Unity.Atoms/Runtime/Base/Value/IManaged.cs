namespace Enderlook.Unity.Atoms
{
    public interface IManaged<TValue> : IGetSet<TValue>, IEventRegister<TValue>, IEventRegister2<TValue>
    {
        /// <summary>
        /// Resets the value stored in this objects to <see cref="InitialValue"/>.
        /// </summary>
        /// <param name="shouldTriggerEvents">Whenever it should trigger set events or not.</param>
        void Reset(bool shouldTriggerEvents = false);

        /// <summary>
        /// Initial value of this variable.
        /// </summary>
        TValue InitialValue { get; }
    }
}