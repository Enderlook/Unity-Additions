using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    /// <summary>
    /// Represent a 2D coordinate.
    /// </summary>
    public class Node : Node<Node, Edge>
    {
        /// <summary>
        /// Position of this node in relation to the graph.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Creates a point in a graph.
        /// </summary>
        /// <param name="position">Position of this node.</param>
        /// <returns>New created node.</returns>
        public Node(Vector2 position) => Position = position;

        /// <inheritdoc cref="Node{TNode, TEdge}.CreateEdgeTo(TNode)"/>
        protected override Edge CreateEdgeTo(Node to) => new Edge(this, to);
    }
}