using System;
using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Serializes information from a graph.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    /// <typeparam name="TNodePayload">Type of additional information stored in nodes.</typeparam>
    /// <typeparam name="TEdgePayload">Type of additional information stored in edges.</typeparam>
    [Serializable]
    public abstract class RootSerializer<TNode, TEdge, TNodePayload, TEdgePayload> : IRootSerializer<TNode, TEdge>
        where TNode : INode<TEdge>, INodeWrite<TNode, TEdge>
        where TEdge : IEdge<TNode>
    {
        [SerializeField]
        private List<TNodePayload> nodes;

        [SerializeField]
        private List<Pair> edgesPair;

        [SerializeField]
        private List<TEdgePayload> edgesPayload;

        /// <inheritdoc cref="IRootSerializer.SerializeRoot(TNode)"/>
        public void SerializeRoot(TNode root)
        {
            if (EqualityComparer<TNode>.Default.Equals(root, default))
                return;

            HashSet<TNode> visited = new HashSet<TNode>();
            Stack<TNode> toVisit = new Stack<TNode>();
            visited.Add(root);
            toVisit.Push(root);

            Dictionary<TNode, int> nodesToId = new Dictionary<TNode, int>();
            nodes = new List<TNodePayload>();
            edgesPair = new List<Pair>();
            edgesPayload = new List<TEdgePayload>();

            for (int i = 0; toVisit.TryPop(out TNode node); i++)
            {
                nodes.Add(Serialize(node));
                nodesToId.Add(node, i);

                foreach (TEdge edge in node.Edges)
                {
                    TNode to = edge.To;
                    if (visited.Contains(to))
                        continue;
                    visited.Add(to);
                    toVisit.Push(to);
                }
            }

            foreach (TNode node in visited)
            {
                foreach (TEdge edge in node.Edges)
                {
                    edgesPair.Add(new Pair(nodesToId[edge.From], nodesToId[edge.To]));
                    edgesPayload.Add(Serialize(edge));
                }
            }
        }

        /// <inheritdoc cref="IRootSerializer{TNode, TEdge}.DeserializeRoot"/>
        public TNode DeserializeRoot()
        {
            if (nodes.Count == 0)
                return default;

            TNode[] deserialized = new TNode[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
                deserialized[i] = Deserialize(nodes[i]);

            int j = 0;
            int f = int.MaxValue;
            for (int k = 0; k < edgesPayload.Count; k++)
            {
                int from = edgesPair[k].from;
                if (f == from)
                    j++;
                else
                {
                    j = 0;
                    f = from;
                }
                deserialized[from].AddEdgeTo(deserialized[edgesPair[k].to]);
                Deserialize(edgesPayload[k], deserialized[from].Edges[j]);
            }

            return deserialized[0];
        }

        /// <summary>
        /// Append serialized information <paramref name="data"/> to reconstructed <paramref name="edge"/>.
        /// </summary>
        /// <param name="data">Serialized information.</param>
        /// <param name="edge">Target to store the information.</param>
        protected abstract void Deserialize(TEdgePayload data, TEdge edge);

        /// <summary>
        /// Reconstruct a node from <paramref name="nodePayload"/>.
        /// </summary>
        /// <param name="nodePayload">Serialized information.</param>
        /// <returns>Deserialized node.</returns>
        protected abstract TNode Deserialize(TNodePayload nodePayload);

        /// <summary>
        /// Extract the payload of <paramref name="edge"/>.
        /// </summary>
        /// <param name="edge">Edge to disarm.</param>
        /// <returns>Extracted information.</returns>
        protected abstract TEdgePayload Serialize(TEdge edge);

        /// <summary>
        /// Extract the payload of <paramref name="node"/>.
        /// </summary>
        /// <param name="node">Node to disarm.</param>
        /// <returns>Extracted information.</returns>
        protected abstract TNodePayload Serialize(TNode node);
    }
}