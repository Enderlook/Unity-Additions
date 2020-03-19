using Enderlook.Unity.Utils.Rects;
using Enderlook.Unity.Utils.UnityEditor;

using System.Reflection;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [CustomPropertyDrawer(typeof(HasConfirmationFieldAttribute))]
    internal class HasConfirmationDrawer : SmartPropertyDrawer
    {
        private bool confirm;

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            HasConfirmationFieldAttribute hasConfirmationFieldAttribute = (HasConfirmationFieldAttribute)attribute;

            object targetObject = property.GetParentTargetObjectOfProperty();

            FieldInfo confirmationField = targetObject.GetType().GetField(hasConfirmationFieldAttribute.confirmFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            string name = ObjectNames.NicifyVariableName(confirmationField.Name);
            string tooltip = ((TooltipAttribute)confirmationField.GetCustomAttribute(typeof(TooltipAttribute), true))?.tooltip;
            confirm = (bool)confirmationField.GetValue(targetObject);
            confirm = EditorGUI.Toggle(new VerticalRectBuilder(position.x, position.y, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight).GetRect(), new GUIContent(name, tooltip ?? ""), confirm);
            confirmationField.SetValue(targetObject, confirm);

            if (confirm)
            {
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(new VerticalRectBuilder(position.x, position.y + EditorGUIUtility.singleLineHeight, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight).GetRect(), property, label, true);
                EditorGUI.indentLevel--;
            }

            property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => confirm ? EditorGUI.GetPropertyHeight(property) + EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
    }
}