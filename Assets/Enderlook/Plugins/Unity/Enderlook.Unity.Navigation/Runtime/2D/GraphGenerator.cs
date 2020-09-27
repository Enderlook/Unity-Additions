using Enderlook.Unity.Extensions;

using System;

using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    /// <inheritdoc cref="GraphGenerator{TGraph, TNode, TEdge, TCoordinate}"/>
    [Serializable]
    public class GraphGenerator : GraphGenerator<Graph, Node, Edge, Vector2>
    {
        /* Based on:
        * http://www.jgallant.com/nodal-pathfinding-in-unity-2d-with-a-in-non-grid-based-games/
        * https://github.com/7ark/Unity-Pathfinding/blob/master/AINavMeshGenerator.cs
        */

        private enum Directions { RIGHT, DOWN_RIGHT, DOWN, DOWN_LEFT, LEFT, UP_LEFT, UP, UP_RIGHT }

        /// <inheritdoc cref="GraphGenerator{TGraph, TNode, TEdge, TCoordinate}.ctor"/>
        public GraphGenerator(Vector2 amount, Vector2 space, Vector2 offset)
            : base(amount, space, offset)
        {
        }

        /// <inheritdoc cref="GraphGenerator{TGraph, TNode, TEdge, TCoordinate}.ctor"/>
        public GraphGenerator()
        {
        }

        /// <inheritdoc cref="GraphGenerator{TGraph, TNode, TEdge, TCoordinate}.Generate(TGraph)"/>
        protected sealed override void Generate(Graph graph)
        {
            if (Amount.x == 0 || Amount.y == 0)
                return;
            Node[] nodes = CreateGrid(Amount.ToVector2Int());
            AddEdgesToNodes(nodes);
            ((IGraphSetter<Node, Edge>)graph).Root = nodes[0];
        }

        private Node[] CreateGrid(Vector2Int amount)
        {
            int nodeAmount = amount.x * amount.y;
            Node[] nodes = new Node[nodeAmount];

            int i = 0;
            for (int x = 0; x < amount.x; x++)
            {
                for (int y = 0; y < amount.y; y++)
                {
                    float width = y * Space.y * 2;
                    if (x % 2 == 0) // Add diamond shape ♦
                        width += Space.y;
                    Vector2 position = new Vector2(width, x * Space.x) + Offset; // Not * 2 as column in order to make diamond shape ♦

                    nodes[i++] = new Node(position);
                }
            }

            return nodes;
        }

        private void AddEdgesToNodes(Node[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                Node node = nodes[i];
                for (int direction = 0; direction < 8; direction++)
                {
                    Node nodeToConnect = GetNodeFromDirection(nodes, i, (Directions)direction);
                    if (nodeToConnect != null)
                        ((INodeWrite<Node, Edge>)node).AddEdgeTo(nodeToConnect);
                }
            }
        }

        private Node GetNodeFromDirection(Node[] nodes, int nodeIndex, Directions direction)
        {
            int columns = (int)Amount.x;
            int index = -1;
            bool isStartOfRow = nodeIndex % columns == 0;
            bool isEndOfRow = (nodeIndex + 1) % columns == 0;
            bool isOddRow = nodeIndex / columns % 2 == 0; // Due to diamond shape ♦

            switch (direction)
            {
                case Directions.RIGHT:
                    if (isEndOfRow) return null;
                    index = nodeIndex + 1;
                    break;
                case Directions.DOWN_RIGHT:
                    if (isEndOfRow && isOddRow) return null;
                    index = nodeIndex - columns + (isOddRow ? 1 : 0);
                    break;
                case Directions.DOWN:
                    index = nodeIndex - (columns * 2);
                    break;
                case Directions.DOWN_LEFT:
                    if (isStartOfRow && !isOddRow) return null;
                    index = nodeIndex - columns - (isOddRow ? 0 : 1);
                    break;
                case Directions.LEFT:
                    if (isStartOfRow) return null;
                    index = nodeIndex - 1;
                    break;
                case Directions.UP_LEFT:
                    if (isStartOfRow && !isOddRow) return null;
                    index = nodeIndex + columns - (isOddRow ? 0 : 1);
                    break;
                case Directions.UP:
                    index = nodeIndex + (columns * 2);
                    break;
                case Directions.UP_RIGHT:
                    if (isEndOfRow && isOddRow) return null;
                    index = nodeIndex + columns + (isOddRow ? 1 : 0);
                    break;
            }

            return index >= 0 && index < nodes.Length ? nodes[index] : null;
        }
    }
}
