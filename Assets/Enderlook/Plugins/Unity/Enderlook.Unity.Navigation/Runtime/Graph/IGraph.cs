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

        /// <summary>
        /// Calculates the path from the closest node from <paramref name="from"/> to the closest node to <paramref name="to"/>.
        /// </summary>
        /// <typeparam name="TGraphPathfinder">Type of pathfinder.</typeparam>
        /// <typeparam name="TPath">Type of path.</typeparam>
        /// <param name="pathfinder">Pathfinder used to calculate the path,</param>
        /// <param name="from">Start location.</param>
        /// <param name="to">Target location.</param>
        /// <param name="path">The path between <paramref name="from"/> and <paramref name="to"/> will be stored here.</param>
        void CalculatePath<TGraphPathfinder, TPath>(TGraphPathfinder pathfinder, TCoordinate from, TCoordinate to, TPath path)
            where TGraphPathfinder : IPathfinder<TPath, TNode, TEdge>
            where TPath : IPathWriter<TNode, TEdge>;
    }
}