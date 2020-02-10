﻿
using Additions.Utils.Rects;

using UnityEditor;

using UnityEngine;

namespace Additions.Serializables.Collections
{
    [CustomPropertyDrawer(typeof(ShowListAttribute), true)]
    internal class SerializableListDrawer : PropertyDrawer
    {
        private bool foldout;

        private VerticalRectBuilder verticalRectBuilder;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            verticalRectBuilder = new VerticalRectBuilder(position.position, new Vector2(position.width, EditorGUIUtility.singleLineHeight));
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty array = property.FindPropertyRelative("array");

            if (foldout = EditorGUI.Foldout(verticalRectBuilder.GetRect(), foldout, label, true))
            {
                array.arraySize = EditorGUI.IntField(verticalRectBuilder.GetRect(), "Size", array.arraySize);

                EditorGUI.indentLevel++;
                for (int i = 0; i < array.arraySize; i++)
                {
                    EditorGUI.PropertyField(verticalRectBuilder.GetRect(), array.GetArrayElementAtIndex(i), true);
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty array = property.FindPropertyRelative("array");
            return EditorGUI.GetPropertyHeight(array, true) + (foldout ? verticalRectBuilder.TotalHeight : 0);
        }
    }
}