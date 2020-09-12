namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represents an interface to mutate a node in a graph.
    /// </summary>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public interface INodeWrite<TNode, TEdge>
    {
        /// <summary>
        /// Add an edge to <paramref name="node"/>.
        /// </summary>
        /// <param name="node">Target of edge.</param>
        void AddEdgeTo(TNode node);

        /// <summary>
        /// Removes the specified edge.
        /// </summary>
        /// <param name="edge">Edge to remove.</param>
        void RemoveEdge(TEdge edge);
    }
}