using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    internal abstract class GraphModifierStorage<TGraph, TNode, TEdge, TCoordinate> : ScriptableObject, IGraphModifier<TGraph, TNode, TEdge, TCoordinate>
            where TGraph : IGraph<TNode, TEdge, TCoordinate>, IGraphSetter<TNode, TEdge>
            where TNode : INode<TEdge>
            where TEdge : IEdge<TNode>
    {
        public abstract void Apply(TGraph graph);
    }
}
