namespace Enderlook.Unity.Atoms
{
    public interface IEventListener<TValue>
    {
        /// <summary>
        /// Executes the stored actions.
        /// </summary>
        void OnEventRaised(TValue parameter);
    }
}