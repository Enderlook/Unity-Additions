namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represent a modifier of a graph.
    /// </summary>
    /// <typeparam name="TGraph">Type of graph.</typeparam>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    /// <typeparam name="TCoordinate">Type of coordinate.</typeparam>
    public interface IGraphModifier<TGraph, TNode, TEdge, TCoordinate>
        where TGraph : IGraph<TNode, TEdge, TCoordinate>, IGraphSetter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        /// <summary>
        /// Apply a modification to <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph">Graph to modify.</param>
        void Apply(TGraph graph);
    }
}