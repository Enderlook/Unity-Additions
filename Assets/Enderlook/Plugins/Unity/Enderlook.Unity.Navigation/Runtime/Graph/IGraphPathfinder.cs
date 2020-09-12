namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represent an interface to make readonly queries to a graph of nodes.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    /// <typeparam name="TCoordinate">Type used to query nodes.</typeparam>
    public interface IGraphPathfinder<TNode, TEdge, TCoordinate> : IGraph<TNode, TEdge, TCoordinate>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        /// <summary>
        /// Calculates the path from <paramref name="from"/> to <paramref name="to"/> using <paramref name="pathfinder"/> and stores it in <paramref name="path"/>.
        /// </summary>
        /// <typeparam name="TGraphPathfinder">Type of pathfinder.</typeparam>
        /// <typeparam name="TPath">Type of path.</typeparam>
        /// <param name="pathfinder">Pathfinder used to find path.</param>
        /// <param name="from">Start location.</param>
        /// <param name="to">End location.</param>
        /// <param name="path">Calculated path.</param>
        void CalculatePath<TGraphPathfinder, TPath>(TGraphPathfinder pathfinder, TCoordinate from, TCoordinate to, TPath path)
            where TGraphPathfinder : IPathfinder<TPath, TNode, TEdge>
            where TPath : IPathWriter<TNode, TEdge>;
    }
}