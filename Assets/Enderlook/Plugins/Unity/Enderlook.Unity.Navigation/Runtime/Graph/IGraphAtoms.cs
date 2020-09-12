using System.Collections.Generic;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represent access to the nodes and edges which composes a graph.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public interface IGraphAtoms<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        /// <summary>
        /// Contains all nodes of the graph.
        /// </summary>
        IReadOnlyCollection<TNode> Nodes { get; }

        /// <summary>
        /// Contains all edges of the graph with their counter-parts.
        /// </summary>
        IReadOnlyCollection<(TEdge, TEdge)> EdgesDoubles { get; }
    }
}