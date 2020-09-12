namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Calculates path between nodes.
    /// </summary>
    /// <typeparam name="TPath">Type of path.</typeparam>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public interface IPathfinder<TPath, TNode, TEdge>
        where TPath : IPathWriter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        /// <summary>
        /// Calculates the path from <paramref name="from"/> to <paramref name="to"/>.
        /// </summary>
        /// <param name="from">Start location.</param>
        /// <param name="to">End location</param>
        /// <param name="path">Resuses the memory of an old path.</param>
        void CalculatePath(TNode from, TNode to, TPath path);
    }
}