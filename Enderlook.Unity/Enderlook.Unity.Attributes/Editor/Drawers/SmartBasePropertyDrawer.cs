using Enderlook.Unity.Utils.UnityEditor;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [CustomPropertyDrawer(typeof(NameAttribute))]
    [CustomPropertyDrawer(typeof(GUIAttribute))]
    [CustomPropertyDrawer(typeof(IndentedAttribute))]
    public class SmartBasePropertyDrawer : PropertyDrawer
    {
        protected SerializedPropertyHelper helper;

        private int identationOffset;

        protected void Before(ref Rect position, ref SerializedProperty property, ref GUIContent label)
        {
            if (helper == null)
                helper = property.GetHelper();
            else
                helper.ResetCycle();

            SerializedPropertyGUIHelper.GetGUIContent(property, ref label);

            IndentedAttribute indentedAttribute = helper.GetAttributeFromField<IndentedAttribute>(true);
            identationOffset = indentedAttribute?.indentationOffset ?? 0;
            EditorGUI.indentLevel += identationOffset;
        }

        protected void After(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.indentLevel -= identationOffset;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Before(ref position, ref property, ref label);
            EditorGUI.PropertyField(position, property);
            After(position, property, label);
        }
    }
}