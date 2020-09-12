namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Serializes information from a graph.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public interface IRootSerializer<TNode, TEdge>
        where TNode : INode<TEdge>, INodeWrite<TNode, TEdge>
        where TEdge : IEdge<TNode>
    {
        /// <summary>
        /// Deserializes the root and all its children.
        /// </summary>
        /// <returns>Root of the graph</returns>
        TNode DeserializeRoot();

        /// <summary>
        /// Serializes the <paramref name="root"/> and all its children.
        /// </summary>
        /// <param name="root">Root of the graph.</param>
        void SerializeRoot(TNode root);
    }
}