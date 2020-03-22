namespace Enderlook.Unity.Atoms
{
    public interface IEventListener2<TValue>
    {
        /// <summary>
        /// Executes the stored actions.
        /// </summary>
        void OnEventRaised(TValue parameter1, TValue parameter2);
    }
}