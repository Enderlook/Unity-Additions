namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represent the conection of one node with another.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    public interface IEdge<TNode>
    {
        /// <summary>
        /// Start of this edge.
        /// </summary>
        TNode From { get; }

        /// <summary>
        /// End of this edge.
        /// </summary>
        TNode To { get; }

        /// <summary>
        /// Cost from traveling from <see cref="From"/> to <see cref="To"/>.
        /// </summary>
        float Cost { get; }
    }
}