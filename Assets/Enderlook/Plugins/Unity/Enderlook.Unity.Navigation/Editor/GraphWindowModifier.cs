namespace Enderlook.Unity.Navigation
{
    internal abstract class GraphWindowModifier<TGraph, TNode, TEdge, TCoordiante> : IGraphModifier<TGraph, TNode, TEdge, TCoordiante>
        where TGraph : IGraph<TNode, TEdge, TCoordiante>, IGraphSetter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        public abstract void OnInspectorGUI();

        public abstract void Apply(TGraph graph);
    }
}
