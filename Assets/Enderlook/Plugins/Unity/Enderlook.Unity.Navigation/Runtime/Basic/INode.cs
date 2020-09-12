using System.Collections.Generic;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represents a node in a graph.
    /// </summary>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public interface INode<TEdge>
    {
        /// <summary>
        /// Get all edges from this node to others.
        /// </summary>
        IReadOnlyList<TEdge> Edges { get; }
    }
}