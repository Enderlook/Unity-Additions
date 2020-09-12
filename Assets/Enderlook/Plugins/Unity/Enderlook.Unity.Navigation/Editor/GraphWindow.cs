using Enderlook.Unity.Utils.UnityEditor;

using System;

using UnityEditor;

using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace Enderlook.Unity.Navigation
{
    internal abstract class GraphWindow<TGraph, TNode, TEdge, TCoordinate, TGraphDrawer> : EditorWindow, IDisposable
        where TGraph : UnityObject, IGraph<TNode, TEdge, TCoordinate>, IGraphAtoms<TNode, TEdge>, IGraphSetter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
        where TGraphDrawer : GraphWindowDrawer<TGraph, TNode, TEdge, TCoordinate>, new()
    {
        private static readonly GUIContent TITLE_CONTENT = new GUIContent("Graph Editor");
        private static readonly GUIContent EXECUTE = new GUIContent("Execute", "Execution behaviour of the modifier");

        // Cached scriptable object editor
        private Editor editor;

        private TGraph graph;
        private TGraphDrawer drawer;

        private SerializedObject[] modifierObjects;
        private string[] modifierLabels;
        private int modifierIndex;

        void IDisposable.Dispose()
        {
            drawer.Dispose();
            Close();
        }

        private void OnDisable() => drawer.Dispose();

        public void SetGraph(TGraph graph)
        {
            this.graph = graph;
            drawer = new TGraphDrawer();
            drawer.Configure(graph, this);
            GraphModifierStorage<TGraph, TNode, TEdge, TCoordinate>[] modifiers = GetModifiers();
            if (!(modifiers is null))
            {
                modifierObjects = new SerializedObject[modifiers.Length];
                modifierLabels = new string[modifierObjects.Length];
                for (int i = 0; i < modifiers.Length; i++)
                {
                    modifierLabels[i] = modifiers[i].name ?? "Modifier";
                    modifierObjects[i] = new SerializedObject(modifiers[i]);
                }
            }
        }

        protected abstract GraphModifierStorage<TGraph, TNode, TEdge, TCoordinate>[] GetModifiers();

        private void OnGUI()
        {
            titleContent = TITLE_CONTENT;
            DrawEditor(graph, ref editor);

            EditorGUIHelper.DrawHorizontalLine();

            drawer.OnInspectorGUI();

            if (!(modifierObjects is null))
            {
                modifierIndex = EditorGUILayout.Popup(new GUIContent("Modifier", "Choose a modifier to apply"), modifierIndex, modifierLabels);
                SerializedObject modifier = modifierObjects[modifierIndex];

                EditorGUIHelper.DrawHorizontalLine();
                EditorGUILayout.PropertyField(modifier.FindProperty("modifier"));
                if (GUILayout.Button(EXECUTE))
                {
                    modifier.ApplyModifiedProperties();
                    ((GraphModifierStorage<TGraph, TNode, TEdge, TCoordinate>)modifier.targetObject).Apply(graph);
                }
            }
        }

        private void DrawEditor(UnityObject obj, ref Editor editor)
        {
            if (editor == null)
                Editor.CreateCachedEditor(obj, null, ref editor);

            // Check again because it may not be created by the Editor.CreateChachedEditor
            if (editor != null)
                editor.OnInspectorGUI();
        }
    }
}
