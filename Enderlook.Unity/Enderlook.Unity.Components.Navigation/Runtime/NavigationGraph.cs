using Enderlook.Unity.Attributes;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Enderlook.Unity.Components.Navigation
{
    [AddComponentMenu("Enderlook/Navigation/Navigation Graph")]
    public class NavigationGraph : MonoBehaviour, IGraphReader, IGraphEditing
    {
        /* Based on:
         * http://www.jgallant.com/nodal-pathfinding-in-unity-2d-with-a-in-non-grid-based-games/
         * https://github.com/7ark/Unity-Pathfinding/blob/master/AINavMeshGenerator.cs
         */
        private enum Directions { RIGHT, DOWN_RIGHT, DOWN, DOWN_LEFT, LEFT, UP_LEFT, UP, UP_RIGHT }

#pragma warning disable CS0649
        [SerializeField, Tooltip("Distance between each node.")]
        private float spacePerNode;

        [SerializeField, Tooltip("Amount of rows.")]
        private int rows;

        [SerializeField, Tooltip("Amount of columns.")]
        private int columns;

        [SerializeField, Tooltip("Layer used to check for collisions.\nIf a collision is found with a node, the node is destroyed.")]
        private LayerMask destroyMask;

        [field: SerializeField, IsProperty, HideInInspector]
        public Graph Graph { get; set; } = new Graph();

        public List<Node> Grid {
            get {
                if (Graph == null)
                    ResetGrid();
                return Graph.Grid;
            }
            private set => Graph.Grid = value;
        }

        public Transform Reference => ((IGraphReader)Graph).Reference;

        public void ResetGrid() => Graph.Grid = new List<Node>();

        public void GenerateGrid()
        {
            FillGrid();
            DeactivateBadNodes();
            SetActiveConnections();
            DeactivateBadConnections();
            DeactivateUnconnectedNodes();
        }

        private void FillGrid()
        {
            int nodeAmount = rows * columns;
            Grid = new List<Node>(nodeAmount);

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                {
                    float width = c * spacePerNode * 2;
                    if (r % 2 == 0) // Add diamond shape ♦
                        width += spacePerNode;
                    Vector2 position = new Vector2(width, r * spacePerNode); // Not * 2 as column in order to make diamond shape ♦
                    Graph.AddNode(position, mode: PositionReference.LOCAL);
                }
            AddConnectionsToNodes();
        }

        private void AddConnectionsToNodes()
        {
            for (int i = 0; i < Grid.Count; i++)
            {
                Node node = Grid[i];
                for (int direction = 0; direction < 8; direction++)
                {
                    Node nodeToConnect = GetNodeFromDirection(i, (Directions)direction);
                    if (nodeToConnect != null)
                        node.AddConnectionTo(nodeToConnect);
                }
            }
        }

        private Node GetNodeFromDirection(int nodeIndex, Directions direction)
        {
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
                    index = nodeIndex - columns * 2;
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
                    index = nodeIndex + columns * 2;
                    break;
                case Directions.UP_RIGHT:
                    if (isEndOfRow && isOddRow) return null;
                    index = nodeIndex + columns + (isOddRow ? 1 : 0);
                    break;
            }

            return index >= 0 && index < Grid.Count ? Grid[index] : null;
        }

        private void DeactivateBadNodes()
        {
            for (int i = Grid.Count - 1; i >= 0; i--)
            {
                Node node = Grid[i];
                // Check if it's overlapping a collider
                Collider2D hit = Physics2D.OverlapCircle(node.position, .1f, destroyMask);
                node.SetActive(hit == null);
            }
        }

        private void SetActiveConnections()
        {
            foreach (Node node in Grid)
                foreach (Connection connection in node.Connections)
                    if (connection != null)
                        connection.SetActive(node.IsActive);
        }

        private void DeactivateBadConnections()
        {
            foreach (Node node in Grid)
                for (int i = 0; i < node.Connections.Count; i++)
                {
                    Connection connection = node.Connections[i];

                    if (connection != null)
                    {
                        bool hit = Physics2D.Linecast(connection.start.position, connection.end.position, destroyMask);
                        node.Connections[i].SetActive(!hit);
                    }
                }
        }

        private void DeactivateUnconnectedNodes()
        {
            foreach (Node node in Grid.Where(e => e.IsActive))
            {
                bool shouldBeDisabled = true;
                foreach (Connection connection in node.Connections)
                {
                    if (connection == null)
                        continue;
                    if (connection.IsActive)
                    {
                        shouldBeDisabled = false;
                        goto End;
                    }

                    foreach (Connection endConnection in connection.end.Connections)
                        if (endConnection != null && endConnection.start == node && endConnection.IsActive)
                        {
                            shouldBeDisabled = false;
                            goto End;
                        }
                }

                End:
                if (shouldBeDisabled)
                    node.SetActive(false);
            }
        }

        public Vector2 GetWorldPosition(Node node) => ((IGraphReader)Graph).GetWorldPosition(node);
        public Vector2 GetLocalPosition(Vector2 position) => ((IGraphReader)Graph).GetLocalPosition(position);
        public void RemoveDuplicatedPositionsFromGrid() => ((IGraphEditing)Graph).RemoveDuplicatedPositionsFromGrid();
        public Node AddNode(Vector2 position, bool isActive = false, PositionReference mode = PositionReference.WORLD) => ((IGraphEditing)Graph).AddNode(position, isActive, mode);
        public void RemoveNodeAndConnections(Node node) => ((IGraphEditing)Graph).RemoveNodeAndConnections(node);
        public void RemoveConnectionsToNothing() => ((IGraphEditing)Graph).RemoveConnectionsToNothing();
        public void AddMissingNodesFromConnections() => ((IGraphEditing)Graph).AddMissingNodesFromConnections();
        public void RemoveNodesWithoutToOrFromConnection() => ((IGraphEditing)Graph).RemoveNodesWithoutToOrFromConnection();
        public void ToggleAllNodes(ToggleMode mode) => ((IGraphEditing)Graph).ToggleAllNodes(mode);
        public void ToggleAllConnections(ToggleMode mode) => ((IGraphEditing)Graph).ToggleAllConnections(mode);
    }
}