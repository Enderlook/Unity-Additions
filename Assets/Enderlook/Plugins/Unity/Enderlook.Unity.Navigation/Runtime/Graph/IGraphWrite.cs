namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Representation of a graph to modify.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public interface IGraphWrite<TNode, TEdge> : IGraphSetter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        /// <summary>
        /// Removes the given node from the graph.<br/>
        /// This method doesn't update cache.
        /// </summary>
        /// <param name="node">Node to remove.</param>
        void RemoveNode(TNode node);

        /// <summary>
        /// Removes the given edge from the graph.<br/>
        /// This method doesn't update cache.
        /// </summary>
        /// <param name="edge">Edge to remove.</param>
        void RemoveEdge(TEdge edge);

        /// <summary>
        /// Expires old cache values.
        /// </summary>
        void ExpireCache();
    }
}