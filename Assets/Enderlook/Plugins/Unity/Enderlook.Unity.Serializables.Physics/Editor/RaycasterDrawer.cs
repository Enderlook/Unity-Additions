﻿using Enderlook.Unity.Utils.UnityEditor;

using System.Reflection;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Physics
{
    [CustomPropertyDrawer(typeof(Raycaster), true), InitializeOnLoad]
    internal class RaycasterDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property);

        static RaycasterDrawer() => SceneView.duringSceneGui += RenderSceneGUI;

        private const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

        private static void RenderSceneGUI(SceneView sceneview)
        {
            foreach ((SerializedProperty serializedProperty, Raycaster rayCasting, Editor _) in PropertyDrawerHelper.FindAllSerializePropertiesInActiveEditorOf<Raycaster>())
                if (serializedProperty.FindPropertyRelative("draw").boolValue)
                {
                    bool edit = serializedProperty.FindPropertyRelative("edit").boolValue;

                    Color color = serializedProperty.FindPropertyRelative("color").colorValue;

                    Vector2 source = rayCasting.Source;

                    Vector2 reference = (Vector2)typeof(Raycaster).GetProperty("Reference", bindingFlags).GetValue(rayCasting);

                    serializedProperty.serializedObject.Update();

                    if (edit)
                        rayCasting.Source = (Vector2)Handles.PositionHandle(source + reference, Quaternion.identity) - reference;

                    Handles.color = color;
                    if (serializedProperty.FindPropertyRelative("distance").floatValue == Mathf.Infinity)
                    {
                        SerializedProperty directionProperty = serializedProperty.FindPropertyRelative("direction");

                        Vector2 direction = directionProperty.vector2Value;

                        // We can't use the WorldEnd property because that will give infinity
                        Vector2 worldEndPosition = source + reference + direction;

                        if (edit)
                            directionProperty.vector2Value = ((Vector2)Handles.PositionHandle(worldEndPosition, Quaternion.identity) - source - reference).normalized;

                        Vector2 sourcePositionWorld = source + reference;

                        Handles.DrawLine(sourcePositionWorld, worldEndPosition);
                        Handles.DrawDottedLine(sourcePositionWorld, worldEndPosition + direction, 1);
                    }
                    else
                    {
                        PropertyInfo worldEndProperty = typeof(Raycaster).GetProperty("WorldEnd", bindingFlags);
                        Vector2 worldEndPosition = (Vector2)worldEndProperty.GetValue(rayCasting);
                        if (edit)
                            worldEndProperty.SetValue(rayCasting, (Vector2)Handles.PositionHandle(worldEndPosition, Quaternion.identity));
                        Handles.DrawLine(reference + source, worldEndPosition);
                    }

                    rayCasting.DrawLine();

                    serializedProperty.serializedObject.ApplyModifiedProperties();
                }
        }
    }
}