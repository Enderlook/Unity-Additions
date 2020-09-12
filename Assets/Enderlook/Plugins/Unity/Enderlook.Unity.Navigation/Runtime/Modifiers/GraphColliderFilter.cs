using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Filter nodes and edges that collides with the specified <see cref="LayerMask"/>.
    /// </summary>
    /// <typeparam name="TGraph">Type of graph.</typeparam>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    /// <typeparam name="TCoordinate">Type of coordinate.</typeparam>
    public abstract class GraphColliderFilter<TGraph, TNode, TEdge, TCoordinate> : IGraphModifier<TGraph, TNode, TEdge, TCoordinate>
        where TGraph : IGraph<TNode, TEdge, TCoordinate>, IGraphSetter<TNode, TEdge>
        where TNode : INode<TEdge>, INodeWrite<TNode, TEdge>
        where TEdge : IEdge<TNode>
    {
        [SerializeField, Tooltip("Anything checked counts as unwalkable.")]
        public LayerMask filter;

        [SerializeField, Tooltip("Radius to check for collision of nodes and edges.")]
        public float checkRadius;

        /// <inheritdoc cref="IGraphModifier{TGraph, TNode, TEdge, TCoordinate}.Apply(TGraph)"/>
        public abstract void Apply(TGraph graph);
    }
}