using Enderlook.Unity.Attributes.AttributeUsage;

using System;

using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represent a procedural generator of a graph.
    /// </summary>
    /// <typeparam name="TGraph">Type of graph.</typeparam>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    /// <typeparam name="TCoordinate">Type of coordinate.</typeparam>
    [Serializable]
    public abstract class GraphGenerator<TGraph, TNode, TEdge, TCoordinate> : IGraphModifier<TGraph, TNode, TEdge, TCoordinate>
        where TGraph : IGraph<TNode, TEdge, TCoordinate>, IGraphSetter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        [SerializeField, Tooltip("Amount of nodes in each axis (rounded down)."), DoNotInspect]
        public TCoordinate Amount;

        [SerializeField, Tooltip("Space between each node."), DoNotInspect]
        public TCoordinate Space;

        [SerializeField, Tooltip("Offset to generate nodes."), DoNotInspect]
        public TCoordinate Offset;

        /// <summary>
        /// Creates a generator of graphs.
        /// </summary>
        /// <param name="amount">Amount of nodes in each axis.</param>
        /// <param name="space">Space between each node.</param>
        /// <param name="offset">Offset to generate nodes.</param>
        protected GraphGenerator(TCoordinate amount, TCoordinate space, TCoordinate offset)
        {
            Amount = amount;
            Space = space;
            Offset = offset;
        }

        /// <summary>
        /// Creates a generator of graphs.
        /// </summary>
        protected GraphGenerator() { }

        /// <summary>
        /// Generate a new graph and store it in <see cref="graph"/>.
        /// </summary>
        public void Apply(TGraph graph)
        {
            graph.Root = default;
            Generate(graph);
        }

        /// <summary>
        /// Generate a new graph and store it in <see cref="graph"/>.
        /// </summary>
        protected abstract void Generate(TGraph graph);
    }
}