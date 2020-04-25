using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [CustomPropertyDrawer(typeof(IndentedAttribute))]
    internal class IndentedDrawer : SmartPropertyDrawer
    {
        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
        {
            IndentedAttribute indentedAttribute = (IndentedAttribute)attribute;
            int indentation = indentedAttribute.indentationOffset;
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.indentLevel += indentation;
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.indentLevel -= indentation;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label);
    }
}
