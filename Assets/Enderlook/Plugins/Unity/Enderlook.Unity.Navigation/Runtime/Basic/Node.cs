using System;
using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    /// <inheritdoc cref="INode{TEdge}"/>
    [Serializable]
    public abstract class Node<TNode, TEdge> : INode<TEdge>, INodeWrite<TNode, TEdge>
    {
        [SerializeField]
        protected List<TEdge> edges;

        /// <inheritdoc cref="INode{TEdge}.Edges"/>
        public IReadOnlyList<TEdge> Edges => edges ?? (IReadOnlyList<TEdge>)Array.Empty<TEdge>();

        /// <inheritdoc cref="INodeWrite{TNode, TEdge}.AddEdgeTo(TNode)"/>
        void INodeWrite<TNode, TEdge>.AddEdgeTo(TNode nodeToConnect)
        {
            if (edges is null)
                edges = new List<TEdge>();

            edges.Add(CreateEdgeTo(nodeToConnect));
        }

        /// <summary>
        /// Creates and edge from this instance to <paramref name="to"/>
        /// </summary>
        /// <param name="to">Target of this edge.</param>
        /// <returns>Created edge.</returns>
        protected abstract TEdge CreateEdgeTo(TNode to);

        /// <inheritdoc cref="INodeWrite{TNode, TEdge}.RemoveEdge(TEdge)"/>
        void INodeWrite<TNode, TEdge>.RemoveEdge(TEdge edge)
        {
            if (!(edges is null))
                edges.Remove(edge);
        }
    }
}