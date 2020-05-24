using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    internal class ReadOnlyDrawer : SmartPropertyDrawer
    {
        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndProperty();
        }

        protected override float GetPropertyHeightSmart(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label);
    }
}