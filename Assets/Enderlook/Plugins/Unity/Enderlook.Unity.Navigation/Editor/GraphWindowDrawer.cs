using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    internal abstract class GraphWindowDrawer<TGraph, TNode, TEdge, TCoordiante> : IDisposable
        where TGraph : IGraph<TNode, TEdge, TCoordiante>, IGraphAtoms<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
    {
        private readonly GUIContent DRAW_NODE_CHECKBOX = new GUIContent("Draw", "Whenever to draw nodes or not.");
        private readonly GUIContent DRAW_ARROWS_CHECKBOX = new GUIContent("Draw Arrow", "Whenever to draw flow arros in edges or not.");
        private readonly GUIContent INSPECT_NODES_CHECKBOX = new GUIContent("Inspect", "Allow inspection of nodes and edges.");
        private readonly GUIContent MOVE_NODES_CHECKBOX = new GUIContent("Move Nodes", "Allow to edit nodes position.");
        private readonly GUIContent MOVE_NODES_POSITION_HANDLE_CHECKBOX = new GUIContent("Use Position Handle", "Use the position handle to move nodes.");
        private static readonly GUIContent REMOVE_NODE = new GUIContent("Remove", "Removes this node from the graph.");
        private static readonly GUIContent REMOVE_EDGE = new GUIContent("Remove", "Removes this edge from the graph.");
        private const float NODE_SIZE = .1f;
        private const float ARROW_SIZE = .1f;

        private Vector3 normal;
        private Quaternion normalQuaternion;
        private Quaternion normalRotationQuaternion;
        private bool draw;
        private bool drawArrows;

        protected virtual bool AllowEditingPosition => false;
        private bool moveNodes;
        private bool positionHandle;

        private bool inspect;
        private enum InspectionType { None, Node, Edge }
        private InspectionType inspection;
        private TNode inspectedNode;
        private TEdge inspectedEdge;

        protected TGraph Graph { get; private set; }

        private EditorWindow window;

        public void Configure(TGraph graph, EditorWindow window)
        {
            this.window = window;
            Graph = graph;
            SceneView.duringSceneGui += OnSceneView;
        }

        public void Dispose() => SceneView.duringSceneGui -= OnSceneView;

        public void OnInspectorGUI()
        {
            if (draw = EditorGUILayout.Toggle(DRAW_NODE_CHECKBOX, draw))
            {
                EditorGUI.indentLevel++;

                drawArrows = EditorGUILayout.Toggle(DRAW_ARROWS_CHECKBOX, drawArrows);

                EditorGUI.BeginDisabledGroup(inspect);
                if (AllowEditingPosition && (moveNodes = EditorGUILayout.Toggle(MOVE_NODES_CHECKBOX, moveNodes)))
                {
                    EditorGUI.indentLevel++;
                    positionHandle = EditorGUILayout.Toggle(MOVE_NODES_POSITION_HANDLE_CHECKBOX, positionHandle);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();

                EditorGUI.BeginDisabledGroup(moveNodes);
                if (inspect = EditorGUILayout.Toggle(INSPECT_NODES_CHECKBOX, inspect))
                {
                    EditorGUI.indentLevel++;
                    switch (inspection)
                    {
                        case InspectionType.Node:
                            OnInspectorGUINode(inspectedNode);
                            if (GUILayout.Button(REMOVE_NODE))
                            {
                                ((IGraphWrite<TNode, TEdge>)Graph).RemoveNode(inspectedNode);
                                ((IGraphWrite<TNode, TEdge>)Graph).ExpireCache();
                                inspection = InspectionType.None;
                            }
                            break;
                        case InspectionType.Edge:
                            OnInspectorGUIEdge(inspectedEdge);
                            if (GUILayout.Button(REMOVE_EDGE))
                            {
                                ((IGraphWrite<TNode, TEdge>)Graph).RemoveEdge(inspectedEdge);
                                ((IGraphWrite<TNode, TEdge>)Graph).ExpireCache();
                                inspection = InspectionType.None;
                            }
                            break;
                    }
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();

                EditorGUI.indentLevel--;
            }
        }

        protected abstract void OnInspectorGUIEdge(TEdge inspectedEdge);

        protected abstract void OnInspectorGUINode(TNode inspectedNode);

        private void OnSceneView(SceneView sceneView)
        {
            Handles.color = Color.blue;

            normal = GetNormal();
            normalQuaternion = Quaternion.Euler(normal.x, normal.y, normal.z);
            normalRotationQuaternion = Quaternion.LookRotation(normal);

            if (draw)
            {
                foreach (TNode node in Graph.Nodes)
                    DrawPoint(node, GetPosition(node));

                foreach ((TEdge from, TEdge to) in Graph.EdgesDoubles)
                    DrawLine(GetPosition(from.From), GetPosition(from.To), (from, to), !EqualityComparer<TEdge>.Default.Equals(to, default));
            }
        }

        protected virtual Vector3 GetNormal() => Vector3.forward;

        protected abstract Vector3 GetPosition(TNode node);

        protected virtual void SetPosition(TNode node, Vector3 newPosition) { }

        private void DrawPoint(TNode node, Vector3 point)
        {
            if (moveNodes && AllowEditingPosition)
            {
                Vector3 newPosition;
                if (positionHandle)
                    newPosition = Handles.DoPositionHandle(point, normalQuaternion);
                else
                    newPosition = Handles.FreeMoveHandle(point, normalQuaternion, NODE_SIZE, Vector3.zero, Handles.CircleHandleCap);

                if (newPosition != point)
                    SetPosition(node, newPosition);
            }
            else
            {
                if (inspect)
                {
                    if (Handles.Button(point, normalRotationQuaternion, NODE_SIZE, NODE_SIZE, Handles.CircleHandleCap))
                    {
                        inspection = InspectionType.Node;
                        inspectedNode = node;
                        window.Repaint();
                    }
                }
                else
                    Handles.DrawSolidDisc(point, normal, NODE_SIZE);
            }
        }

        private void DrawLine(Vector3 from, Vector3 to, (TEdge, TEdge) pair, bool doubleSide)
        {
            Handles.DrawLine(from, to);

            if (drawArrows)
            {
                Arrow(from, to, pair.Item1);
                if (doubleSide)
                    Arrow(to, from, pair.Item2);
            }

            void Arrow(Vector3 a, Vector3 b, TEdge edge)
            {
                Vector3 half = (a + (b * 2)) / 3;
                Vector3 normalized = (a - b).normalized;
                if (inspect)
                {
                    if (Handles.Button(half, Quaternion.LookRotation(-normalized), ARROW_SIZE, ARROW_SIZE, Handles.ConeHandleCap))
                    {
                        inspection = InspectionType.Edge;
                        inspectedEdge = edge;
                        window.Repaint();
                    }
                }
                else
                {
                    Handles.DrawSolidArc(half, normal, normalized, 35, ARROW_SIZE);
                    Handles.DrawSolidArc(half, normal, normalized, -35, ARROW_SIZE);
                }
            }
        }
    }
}
