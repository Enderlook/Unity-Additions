using System.Collections.Generic;
using System.Diagnostics;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Represents a path.
    /// </summary>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public class Path<TNode, TEdge> : IPathReader<TNode>, IPathWriter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        /// <inheritdoc cref="IPathReader{TNode, TEdge}.From"/>
        public TNode From { get; private set; }

        /// <inheritdoc cref="IPathReader{TNode, TEdge}.To"/>
        public TNode To { get; private set; }

        /// <inheritdoc cref="IPathReader{TNode, TEdge}.TotalCost"/>
        public float TotalCost { get; private set; }

        /// <inheritdoc cref="IPathReader{TNode, TEdge}.FoundPath"/>
        public bool FoundPath { get; private set; }

        /// <summary>
        /// Stores the cumulative cost to reach from <see cref="From"/> to <see cref="To"/>.
        /// </summary>
        private Dictionary<TNode, float> costs = new Dictionary<TNode, float>();

        /// <summary>
        /// Stores from which edge must the node be reached.
        /// </summary>
        private Dictionary<TNode, TEdge> edges = new Dictionary<TNode, TEdge>();

        /// <summary>
        /// Stores the path of nodes to follow.
        /// </summary>
        private List<TNode> pathList = new List<TNode>();

        /// <inheritdoc cref="IPathWriter{TNode, TEdge}.Reset"/>
        public void Reset()
        {
            costs.Clear();
            edges.Clear();
            pathList.Clear();
            From = default;
            To = default;
        }

        /// <inheritdoc cref="IPathWriter{TNode, TEdge}.SetCostTo(TNode, float)"/>
        public void SetCostTo(TNode to, float cost) => costs[to] = cost;

        /// <inheritdoc cref="IPathWriter{TNode, TEdge}.SetEdgeTo(TNode, TEdge)"/>
        public void SetEdgeTo(TNode to, TEdge edge) => edges[to] = edge;

        /// <inheritdoc cref="IPathWriter{TNode, TEdge}.SetFromTo(TNode, TNode)"/>
        public void SetFromTo(TNode from, TNode to)
        {
            From = from;
            To = to;
        }

        /// <inheritdoc cref="IPathWriter{TNode, TEdge}.TryGetCostTo(TNode, out float)"/>
        public bool TryGetCostTo(TNode node, out float cost) => costs.TryGetValue(node, out cost);

        /// <inheritdoc cref="IPathWriter{TNode, TEdge}.Complete(bool)"/>
        public void Complete(bool success)
        {
            if (success)
            {
                FoundPath = true;
                TotalCost = costs[To];
            }
            else
            {
                FoundPath = false;
                TotalCost = float.PositiveInfinity;
            }
        }

        /// <inheritdoc cref="IPathIndexing{T}.GetValueAt(int)"/>
        public TNode GetValueAt(int index)
        {
            Debug.Assert(FoundPath);

            if (pathList.Count == 0)
                CalculatePathList();
            return pathList[index];
        }

        /// <inheritdoc cref="IPathIndexing{T}.TryGetValueAt(int, out T)"/>
        public bool TryGetValueAt(int index, out TNode value)
        {
            Debug.Assert(FoundPath);

            if (pathList.Count == 0)
                CalculatePathList();
            if (index < pathList.Count)
            {
                value = pathList[index];
                return true;
            }
            value = default;
            return false;
        }

        /// <inheritdoc cref="IPathIndexing{T}.TryGetNext(ref int, out T)"/>
        public bool TryGetNext(ref int index, out TNode value)
        {
            Debug.Assert(FoundPath);

            if (pathList.Count == 0)
                CalculatePathList();

            if (index < pathList.Count)
            {
                value = pathList[index++];
                return true;
            }

            value = default;
            return false;
        }

        private void CalculatePathList()
        {
            Debug.Assert(FoundPath);

            TNode node = To;
            pathList.Add(node);
            while (!EqualityComparer<TNode>.Default.Equals(node, From))
            {
                node = edges[node].From;
                pathList.Add(node);
            }
            pathList.Reverse();
        }
    }
}