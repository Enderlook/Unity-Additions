using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    /// <summary>
    /// Represent an edge between two <see cref="Node"/>.
    /// </summary>
    public class Edge : Edge<Node>
    {
        /// <inheritdoc cref="IEdge{TNode}.Cost"/>
        public override float Cost => Vector2.Distance(From.Position, To.Position);

        /// <summary>
        /// Creates an edge between two nodes.
        /// </summary>
        /// <param name="from">From which node this edge extends.</param>
        /// <param name="to">To which node this edge connects.</param>
        /// <returns>New created node.</returns>
        internal Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }
    }
}