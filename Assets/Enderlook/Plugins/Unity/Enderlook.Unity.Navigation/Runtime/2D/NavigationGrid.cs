using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    [ExecuteAlways]
    public class NavigationGrid : MonoBehaviour, IGraph<Node, Edge, Vector2>
    {
        [SerializeField, Min(0), Tooltip("Size of each square.")]
        private float cellSize;

        [SerializeField, Tooltip("Amount of squares in the grid.")]
        private Vector2Int gridSize;

        [SerializeField, Tooltip("Whenever allow diagonals or not.")]
        private bool allowDiagonals;

        [SerializeField, Tooltip("Layers that block path.")]
        private LayerMask unwalkable;

        [SerializeField, Tooltip("Navigation graph.")]
        private Graph graph;

        public void CalculatePath<TGraphPathfinder, TPath>(TGraphPathfinder pathfinder, Vector2 from, Vector2 to, TPath path)
            where TGraphPathfinder : IPathfinder<TPath, Node, Edge>
            where TPath : IPathWriter<Node, Edge> => graph.CalculatePath(pathfinder, from, to, path);

        public Node FindClosestNode(Vector2 value) => graph.FindClosestNode(value);

        public void RegenerateGraph()
        {
            Vector2 boundaries = ((Vector2)gridSize) * cellSize;

            Dictionary<Vector3, Node> nodes = new Dictionary<Vector3, Node>();
            HashSet<(Node, Node)> connections = new HashSet<(Node, Node)>();
            HashSet<Node> visited = new HashSet<Node>();

            Stack<Node> toVisit = new Stack<Node>();
            Node root = new Node(new Vector2(cellSize, cellSize));
            toVisit.Push(root);
            nodes.Add(root.Position, root);

            while (toVisit.TryPop(out Node node))
            {
                visited.Add(node);

                Process(1, 0);
                Process(0, 1);

                if (allowDiagonals)
                    Process(1, 1);

                void Process(float x, float y)
                {
                    Vector2 position = node.Position;
                    x = position.x + (x * cellSize);
                    y = position.y + (y * cellSize);
                    if (x >= boundaries.x || y >= boundaries.y || x <= 0 || y <= 0)
                        return;

                    Vector2 newPosition = new Vector2(x, y);
                    if (Physics2D.Linecast(position, newPosition, unwalkable))
                        return;

                    if (!nodes.TryGetValue(newPosition, out Node target))
                    {
                        target = new Node(newPosition);
                        nodes.Add(newPosition, target);
                        toVisit.Push(target);
                        CreateConnections();
                    }
                    else if (!connections.Contains((node, target)))
                        CreateConnections();

                    void CreateConnections()
                    {
                        connections.Add((node, target));
                        ((INodeWrite<Node, Edge>)node).AddEdgeTo(target);

                        connections.Add((target, node));
                        ((INodeWrite<Node, Edge>)target).AddEdgeTo(node);
                    }
                }
            }

            ((IGraphSetter<Node, Edge>)graph).Root = root;
        }

#if UNITY_EDITOR
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnValidate()
        {
            if (graph == null)
                graph = ScriptableObject.CreateInstance<Graph>();

            if (gridSize.x < 1)
                gridSize.x = 1;
            if (gridSize.y < 1)
                gridSize.y = 1;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector3 local = new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2) + graph.TweakOrientationToLocal(transform.position);

                    Vector3 from = graph.TweakOrientationToWorld(local);
                    if (y > 0)
                        Gizmos.DrawLine(from, graph.TweakOrientationToWorld(local - new Vector3(0, cellSize)));
                    if (x > 0)
                        Gizmos.DrawLine(from, graph.TweakOrientationToWorld(local - new Vector3(cellSize, 0)));
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Update()
        {
            foreach (Transform child in transform)
            {
                Vector3 position = child.localPosition;
                child.localPosition = new Vector3(Round(position.x), Round(position.y), Round(position.z));
            }

            float Round(float value) => (int)(value / cellSize) * cellSize;
        }
#endif
    }
}