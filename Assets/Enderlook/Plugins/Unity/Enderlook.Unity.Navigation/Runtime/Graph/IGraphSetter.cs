namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Representation of a graph to set.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public interface IGraphSetter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        /// <summary>
        /// Root of graph.
        /// </summary>
        TNode Root { get; set; }
    }
}