using System;
using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represent a graph of nodes.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    /// <typeparam name="TCoordinate">Type used to query nodes.</typeparam>
    /// <typeparam name="TRootSerializer">Type of root serializer.</typeparam>
    [Serializable]
    public abstract class Graph<TNode, TEdge, TCoordinate, TRootSerializer>
        : ScriptableObject,
            IGraphAtoms<TNode, TEdge>,
            IGraphSetter<TNode, TEdge>,
            IGraphPathfinder<TNode, TEdge, TCoordinate>,
            IGraphWrite<TNode, TEdge>,
            ISerializationCallbackReceiver
        where TNode : INode<TEdge>, INodeWrite<TNode, TEdge>
        where TEdge : IEdge<TNode>
        where TRootSerializer : IRootSerializer<TNode, TEdge>, new()
    {
        [SerializeField, HideInInspector]
        private TRootSerializer serializer;

        private TNode root;

        private GraphCacher<TNode, TEdge> cacher;

        /// <inheritdoc cref="IGraphSetter{TNode, TEdge}.Root"/>
        TNode IGraphSetter<TNode, TEdge>.Root {
            get => root;
            set {
                root = value;
                cacher.SetRoot(root);
            }
        }

        /// <inheritdoc cref="GraphCacher{TNode, TEdge}.Nodes"/>
        IReadOnlyCollection<TNode> IGraphAtoms<TNode, TEdge>.Nodes => cacher.Nodes;

        /// <inheritdoc cref="GraphCacher{TNode, TEdge}.EdgesDoubles"/>
        IReadOnlyCollection<(TEdge, TEdge)> IGraphAtoms<TNode, TEdge>.EdgesDoubles => cacher.EdgesDoubles;

        /// <inheritdoc cref="IGraph{TNode, TEdge, T}.FindClosestNode(T)"/>
        public TNode FindClosestNode(TCoordinate value)
        {
            float distance = float.PositiveInfinity;
            TNode closest = default;
            foreach (TNode node in cacher.Nodes)
            {
                float newDistance = GetDistance(node, value);
                if (distance > newDistance)
                {
                    distance = newDistance;
                    closest = node;
                }
            }

            return closest;
        }

        /// <summary>
        /// Calculates the distance between <paramref name="node"/> and <paramref name="value"/>.
        /// </summary>
        /// <param name="node">Node to compare value.</param>
        /// <param name="value">Value looked.</param>
        /// <returns>Distance from <paramref name="node"/> to <paramref name="value"/>.</returns>
        protected abstract float GetDistance(TNode node, TCoordinate value);

        /// <inheritdoc cref="IGraph{TNode, TEdge, T}.CalculatePath{TGraphPathfinder, TPath}(TGraphPathfinder, T, T, TPath)"/>
        public void CalculatePath<TGraphPathfinder, TPath>(TGraphPathfinder pathfinder, TCoordinate from, TCoordinate to, TPath path)
            where TGraphPathfinder : IPathfinder<TPath, TNode, TEdge>
            where TPath : IPathWriter<TNode, TEdge>
            => pathfinder.CalculatePath(FindClosestNode(from), FindClosestNode(to), path);

        /// <inheritdoc cref="ISerializationCallbackReceiver.OnBeforeSerialize"/>
        void ISerializationCallbackReceiver.OnBeforeSerialize() => (serializer = new TRootSerializer()).SerializeRoot(root);

        /// <inheritdoc cref="ISerializationCallbackReceiver.OnAfterDeserialize"/>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            root = serializer.DeserializeRoot();
            cacher.SetRoot(root);
            serializer = default;
        }

        /// <inheritdoc cref="IGraphWrite{TNode, TEdge}.RemoveNode(TNode)"/>
        void IGraphWrite<TNode, TEdge>.RemoveNode(TNode node)
        {
            foreach ((TEdge a, TEdge b) in cacher.EdgesDoubles)
            {
                if (EqualityComparer<TNode>.Default.Equals(a.To, node))
                    a.From.RemoveEdge(a);
                if (EqualityComparer<TNode>.Default.Equals(b.To, node))
                    b.From.RemoveEdge(b);
            }
        }

        /// <inheritdoc cref="IGraphWrite{TNode, TEdge}.RemoveEdge(TEdge)"/>
        void IGraphWrite<TNode, TEdge>.RemoveEdge(TEdge edge) => edge.From.RemoveEdge(edge);

        /// <inheritdoc cref="IGraphWrite{TNode, TEdge}.ExpireCache"/>
        void IGraphWrite<TNode, TEdge>.ExpireCache() => cacher.ExpireCache();
    }
}