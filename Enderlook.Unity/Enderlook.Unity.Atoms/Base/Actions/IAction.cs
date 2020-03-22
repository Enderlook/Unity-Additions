namespace Enderlook.Unity.Atoms
{
    public interface IAction<TValue>
    {
        /// <summary>
        /// Execute action.
        /// </summary>
        /// <param name="parameter">Parameter passed to the action.</param>
        void Execute(TValue parameter);
    }
}