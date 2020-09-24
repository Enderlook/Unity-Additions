using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    [CustomEditor(typeof(Graph))]
    internal sealed class Graph2DEditor : GraphEditor<Graph, Node, Edge, Vector2, Graph2DEditor.GraphWindowDrawer, Graph2DEditor.GraphWindow>
    {
        internal sealed class GraphWindow : GraphWindow<Graph, Node, Edge, Vector2, GraphWindowDrawer>
        {
            protected override GraphModifierStorage<Graph, Node, Edge, Vector2>[] GetModifiers()
            {
                GraphGeneratorSO graphGenerator = CreateInstance<GraphGeneratorSO>();
                graphGenerator.modifier = new GraphGenerator();
                graphGenerator.name = "Graph Generator";

                GraphColliderFilterSO graphColliderFilter = CreateInstance<GraphColliderFilterSO>();
                graphColliderFilter.modifier = new GraphColliderFilter();
                graphColliderFilter.name = "Graph Collider Filter";

                return new GraphModifierStorage<Graph, Node, Edge, Vector2>[]
                {
                    graphGenerator,
                    graphColliderFilter,
                };
            }
        }

        internal sealed class GraphWindowDrawer : GraphWindowDrawer<Graph, Node, Edge, Vector2>
        {
            private static readonly GUIContent POSITION = new GUIContent("Position", "World position of this node.");
            private static readonly GUIContent FROM_LABEL = new GUIContent("From", "Beginning of this edge.");
            private static readonly GUIContent TO_LABEL = new GUIContent("TO", "End of this edge.");
            private static readonly GUIContent COST = new GUIContent("Cost", "Cost of traveling this edge.");

            protected override bool AllowEditingPosition => true;

            protected override Vector3 GetPosition(Node node)
                => Graph.TweakOrientationToWorld(node.Position);

            protected override Vector3 GetNormal()
                => Graph.GetNormal();

            protected override void SetPosition(Node node, Vector3 newPosition)
                => node.Position = Graph.TweakOrientationToLocal(newPosition);

            protected override void OnInspectorGUIEdge(Edge inspectedEdge)
            {
                EditorGUILayout.LabelField(COST, new GUIContent(inspectedEdge.Cost.ToString()));
                EditorGUILayout.LabelField(FROM_LABEL);
                EditorGUI.indentLevel++;
                OnInspectorGUINode(inspectedEdge.From);
                EditorGUI.indentLevel--;

                EditorGUILayout.LabelField(TO_LABEL);
                EditorGUI.indentLevel++;
                OnInspectorGUINode(inspectedEdge.To);
                EditorGUI.indentLevel--;
            }

            protected override void OnInspectorGUINode(Node inspectedNode)
            {
                Vector3 world = Graph.TweakOrientationToWorld(inspectedNode.Position);

                Vector3 newPosition = EditorGUILayout.Vector3Field(POSITION, world);
                if (world != newPosition)
                    inspectedNode.Position = Graph.TweakOrientationToLocal(newPosition);
            }
        }
    }

    internal class GraphGeneratorSO : GraphModifierStorage<Graph, Node, Edge, Vector2>
    {
        public GraphGenerator modifier;

        public override void Apply(Graph graph) => modifier.Apply(graph);
    }

    internal class GraphColliderFilterSO : GraphModifierStorage<Graph, Node, Edge, Vector2>
    {
        public GraphColliderFilter modifier;

        public override void Apply(Graph graph) => modifier.Apply(graph);
    }
}
