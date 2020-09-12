using System;

using UnityEditor;

using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace Enderlook.Unity.Navigation
{
    internal class GraphEditor<TGraph, TNode, TEdge, TCoordinate, TGraphDrawer, TGraphWindow> : Editor
        where TGraph : UnityObject, IGraph<TNode, TEdge, TCoordinate>, IGraphAtoms<TNode, TEdge>, IGraphSetter<TNode, TEdge>
        where TNode : INode<TEdge>
        where TEdge : IEdge<TNode>
        where TGraphDrawer : GraphWindowDrawer<TGraph, TNode, TEdge, TCoordinate>, new()
        where TGraphWindow : GraphWindow<TGraph, TNode, TEdge, TCoordinate, TGraphDrawer>
    {
        private static readonly GUIContent BUTTON = new GUIContent("Open Editor", "Open the editor window.");

        private TGraphWindow window;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button(BUTTON))
            {
                window = EditorWindow.CreateWindow<TGraphWindow>();
                window.SetGraph((TGraph)target);
            }
        }

        public void OnDisable()
        {
            if (window != null)
                ((IDisposable)window).Dispose();
        }
    }
}
