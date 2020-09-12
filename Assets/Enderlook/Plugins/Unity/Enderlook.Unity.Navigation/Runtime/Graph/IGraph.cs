namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represent an interface to make readonly queries to a graph of nodes.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    /// <typeparam name="TCoordinate">Type used to query nodes.</typeparam>
    public interface IGraph<TNode, TEdge, TCoordinate>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        /// <summary>
        /// Returns the node which closest matches the value <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Value used to query.</param>
        /// <returns>The closest node to match <paramref name="value"/>.</returns>
        TNode FindClosestNode(TCoordinate value);
    }
}