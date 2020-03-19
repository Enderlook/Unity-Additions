using Enderlook.Unity.Utils.UnityEditor;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [CustomPropertyDrawer(typeof(NameAttribute))]
    [CustomPropertyDrawer(typeof(GUIAttribute))]
    public class SmartBasePropertyDrawer : PropertyDrawer
    {
        protected SerializedPropertyHelper helper;

        protected void Initialize(ref Rect position, ref SerializedProperty property, ref GUIContent label)
        {
            if (helper == null)
                helper = property.GetHelper();
            else
                helper.ResetCycle();

            SerializedPropertyGUIHelper.GetGUIContent(property, ref label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize(ref position, ref property, ref label);
            EditorGUI.PropertyField(position, property, label);
        }
    }
}