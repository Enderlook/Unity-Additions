using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    [CustomEditor(typeof(NavigationGrid))]
    internal class NavigationGridEditor : Editor
    {
        GUIContent REGENERATE_BUTTON = new GUIContent("Regenerate", "Regenerates the graph.");

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button(REGENERATE_BUTTON))
                ((NavigationGrid)target).RegenerateGraph();
        }
    }
}