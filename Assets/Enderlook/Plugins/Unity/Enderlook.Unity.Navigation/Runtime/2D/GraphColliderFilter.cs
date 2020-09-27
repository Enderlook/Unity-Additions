using System;

using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    /// <inheritdoc cref="GraphColliderFilter{TGraph, TNode, TEdge, TCoordinate}"/>
    [Serializable]
    public class GraphColliderFilter : GraphColliderFilter<Graph, Node, Edge, Vector2>
    {
        /// <inheritdoc cref="IGraphModifier{TGraph, TNode, TEdge, TCoordinate}.Apply(TGraph)"/>
        public override void Apply(Graph graph)
        {
            IGraphAtoms<Node, Edge> atoms = graph;
            IGraphWrite<Node, Edge> modifier = graph;

            foreach ((Edge a, Edge b) in atoms.EdgesDoubles)
            {
                Vector3 from = graph.TweakOrientationToWorld(a.From.Position);
                Vector3 to = graph.TweakOrientationToWorld(a.To.Position);
                Vector3 direction = to - from;
                if (Physics.SphereCast(from, checkRadius, direction.normalized, out RaycastHit _, direction.magnitude, filter))
                {
                    modifier.RemoveEdge(a);
                    modifier.RemoveEdge(b);
                }
            }

            foreach (Node node in atoms.Nodes)
            {
                Vector3 position = graph.TweakOrientationToWorld(node.Position);
                if (Physics.CheckSphere(position, checkRadius, filter))
                    modifier.RemoveNode(node);
            }

            modifier.ExpireCache();
        }
    }
}
