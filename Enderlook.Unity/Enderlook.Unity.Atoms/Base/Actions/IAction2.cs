namespace Enderlook.Unity.Atoms
{
    public interface IAction2<TValue>
    {
        /// <summary>
        /// Execute action.
        /// </summary>
        /// <param name="parameter1">First parameter passed to the action.</param>
        /// <param name="parameter2">Second parameter passed to the action.</param>
        void Execute(TValue parameter1, TValue parameter2);
    }
}