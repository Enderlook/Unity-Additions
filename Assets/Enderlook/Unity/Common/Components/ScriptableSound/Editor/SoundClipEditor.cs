using Enderlook.Unity.Utils.UnityEditor;
using System;

using UnityEditor;

using UnityEngine;

using UnityObject = UnityEngine.Object;
namespace Enderlook.Unity.Components.ScriptableSound
{
    [CustomEditor(typeof(SoundClip))]
    internal class SoundClipEditor : Editor
    {
        private static readonly GUIContent DURATION_CONTENT = new GUIContent("Duration", "Duration of the clip.");

        public override void OnInspectorGUI()
        {
            this.DrawScriptField();

            EditorGUI.BeginChangeCheck();

            SerializedProperty audioClipProperty = serializedObject.FindProperty("audioClip");
            EditorGUILayout.PropertyField(audioClipProperty, true);
            UnityObject audioClip = audioClipProperty.objectReferenceValue;
            if (audioClip != null)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.TextField(DURATION_CONTENT, TimeSpan.FromSeconds(((AudioClip)audioClip).length).ToString(@"hh\:mm\:ss\:ff"));
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("modifiers"), true);
            SoundPlayerEditor.DrawAmountField(serializedObject.FindProperty("playsAmount"));

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
    }
}