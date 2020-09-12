namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Representation of a readable path.
    /// </summary>
    public interface IPathReader<T> : IPathIndexing<T>
    {
        /// <summary>
        /// Determines the initial node.
        /// </summary>
        T From { get; }

        /// <summary>
        /// Determines the target node.
        /// </summary>
        T To { get; }

        /// <summary>
        /// Determines the total cost of travelling from <see cref="From"/> to <see cref="To"/>.
        /// </summary>
        float TotalCost { get; }

        /// <summary>
        /// Whenever there is a path <see cref="From"/> to <see cref="To"/>.
        /// </summary>
        bool FoundPath { get; }
    }
}