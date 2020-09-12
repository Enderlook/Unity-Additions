using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Caching layer arround a <see cref="root"/> to query nodes and edges.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public struct GraphCacher<TNode, TEdge>
        where TNode : INode<TEdge>, INodeWrite<TNode, TEdge>
        where TEdge : IEdge<TNode>
    {
        private TNode root;

        /// <summary>
        /// Represents a quick access to all nodes.
        /// </summary>
        private HashSet<TNode> nodes;

        /// <inheritdoc cref="IGraphAtoms{TNode, TEdge}.Nodes"/>
        public IReadOnlyCollection<TNode> Nodes {
            get {
                if (nodes.Count == 0)
                    CacheNodeList();
                return nodes;
            }
        }

        /// <summary>
        /// Represents a quick access to all edges in a double direction mode.
        /// </summary>
        private HashSet<(TEdge, TEdge)> edgesDouble;

        /// <inheritdoc cref="IGraphAtoms{TNode, TEdge}.EdgesDoubles"/>
        public IReadOnlyCollection<(TEdge, TEdge)> EdgesDoubles {
            get {
                if (edgesDouble.Count == 0)
                    CacheDoubleEdgeList();
                return edgesDouble;
            }
        }

        /// <summary>
        /// Determines which root to cache.
        /// </summary>
        public void SetRoot(TNode root)
        {
            this.root = root;

            if (nodes == null)
                nodes = new HashSet<TNode>();
            else
                nodes.Clear();

            if (edgesDouble == null)
                edgesDouble = new HashSet<(TEdge, TEdge)>();
            else
                edgesDouble.Clear();
        }

        /// <summary>
        /// Expires all cache.
        /// </summary>
        public void ExpireCache()
        {
            nodes.Clear();
            edgesDouble.Clear();
        }

        /// <summary>
        /// Constructs a list with all nodes from <see cref="root"/> for easier lookups.
        /// </summary>
        private void CacheNodeList()
        {
            if (EqualityComparer<TNode>.Default.Equals(root, default))
                return;

            nodes.Add(root);

            Stack<TNode> toSearch = new Stack<TNode>();
            toSearch.Push(root);

            while (toSearch.TryPop(out TNode node))
            {
                nodes.Add(node);
                foreach (TEdge edge in node.Edges)
                {
                    TNode a = edge.From;
                    if (!nodes.Contains(a))
                        toSearch.Push(a);

                    TNode b = edge.To;
                    if (!nodes.Contains(b))
                        toSearch.Push(b);
                }
            }
        }

        /// <summary>
        /// Constructs a list with all edges from <see cref="Nodes"/>.
        /// </summary>
        private void CacheDoubleEdgeList()
        {
            HashSet<TEdge> edges = new HashSet<TEdge>();
            foreach (TNode node in nodes)
            {
                foreach (TEdge edge in node.Edges)
                {
                    if (edges.Contains(edge))
                        continue;
                    foreach (TEdge counter in edge.To.Edges)
                    {
                        if (EqualityComparer<TNode>.Default.Equals(counter.To, edge.From))
                        {
                            if (edges.Contains(counter))
                                continue;
                            edges.Add(counter);

                            edgesDouble.Add((edge, counter));

                            goto Next;
                        }
                    }
                    edgesDouble.Add((edge, default));
                    Next: { }
                }
            }
        }
    }
}