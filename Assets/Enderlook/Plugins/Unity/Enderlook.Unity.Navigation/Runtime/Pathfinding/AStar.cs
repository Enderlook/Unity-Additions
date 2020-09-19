using Enderlook.Collections;

using System.Collections.Generic;

namespace Enderlook.Unity.Navigation
{
    /// <summary>
    /// Algorithm used to find paths between nodes.
    /// </summary>
    /// <typeparam name="TPath">Type of produced path.</typeparam>
    /// <typeparam name="TNode">Type of node.</typeparam>
    /// <typeparam name="TEdge">Type of edge.</typeparam>
    public class AStar<TPath, TNode, TEdge> : IPathfinder<TPath, TNode, TEdge>
        where TPath : IPathWriter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        private HashSet<TNode> visited = new HashSet<TNode>();

        private BinaryHeapMin<TNode, float> toVisit = new BinaryHeapMin<TNode, float>();

        /* https://code.msdn.microsoft.com/windowsdesktop/Dijkstras-Single-Soruce-69faddb3
        * https://www.geeksforgeeks.org/csharp-program-for-dijkstras-shortest-path-algorithm-greedy-algo-7/
        * https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
        * http://theory.stanford.edu/~amitp/GameProgramming/AStarComparison.html
        * https://www.redblobgames.com/pathfinding/a-star/introduction.html
        */

        private void Clear()
        {
            visited.Clear();
            toVisit.Clear();
        }

        /// <inheritdoc cref="IPathfinder{TPath, TNode, TEdge}.CalculatePath(TNode, TNode, TPath)"/>
        public void CalculatePath(TNode from, TNode to, TPath path)
        {
            path.Reset();
            path.SetFromTo(from, to);
            path.SetCostTo(from, 0);

            visited = new HashSet<TNode>();
            toVisit = new BinaryHeapMin<TNode, float>();
            toVisit.Enqueue(from, 0);

            while (toVisit.TryDequeue(out TNode node, out _))
            {
                if (visited.Contains(node))
                    continue;
                visited.Add(node);

                float distanceFromSource;
                if (path.TryGetCostTo(node, out float cost))
                    distanceFromSource = cost;
                else
                    distanceFromSource = float.PositiveInfinity;

                foreach (TEdge edge in node.Edges)
                {
                    TNode neighbour = edge.To;
                    float distance = Relax(path, edge, distanceFromSource);

                    toVisit.Enqueue(neighbour, distance + Heuristic(neighbour, from));

                    if (EqualityComparer<TNode>.Default.Equals(neighbour, to))
                    {
                        path.Complete(true);
                        Clear();
                        return;
                    }
                }
            }

            path.Complete(false);
            Clear();
        }

        /// <summary>
        /// Heuristic formula to guess distance between two nodes.
        /// </summary>
        /// <param name="to">Start node.</param>
        /// <param name="from">Target node.</param>
        /// <returns>Guessed distance.</returns>
        protected virtual float Heuristic(TNode to, TNode from) => 0;

        private float Relax(TPath path, TEdge edge, float distanceFromSource)
        {
            float newDistance = edge.Cost + distanceFromSource;
            if (!path.TryGetCostTo(edge.To, out float cost) || newDistance < cost)
            {
                path.SetCostTo(edge.To, newDistance);
                path.SetEdgeTo(edge.To, edge);
            }
            return newDistance;
        }
    }
}