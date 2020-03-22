using Enderlook.Extensions;
using Enderlook.Unity.Attributes;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    internal class ReferenceDrawer
    {
        private static string modeProperty = ReflectionExtesions.GetBackingFieldName("Mode");

        protected (string displayName, string propertyName, int enumValue)[] modes;

        private static GUIStyle popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
        {
            imagePosition = ImagePosition.ImageOnly
        };

        private string[] popupOptions;

        public ReferenceDrawer(params (string displayName, string propertyName, int enumValue)[] modes)
        {
            this.modes = modes;
            popupOptions = modes.Select(e => e.displayName).ToArray();
        }

        public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            // Show field label
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // Get current mode
            SerializedProperty mode = property.FindPropertyRelative(modeProperty);
            int popupIndex = GetPopupIndex(mode);

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(
                position.x,
                position.y + popupStyle.margin.top,
                popupStyle.fixedWidth + popupStyle.margin.right,
                position.height - popupStyle.margin.top);
            position.xMin = buttonRect.xMax;

            int newUsagePopupIndex = EditorGUI.Popup(buttonRect, popupIndex, popupOptions, popupStyle);
            mode.intValue = modes[newUsagePopupIndex].enumValue;

            EditorGUI.PropertyField(position,
                property.FindPropertyRelative(modes[newUsagePopupIndex].propertyName),
                GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        private int GetPopupIndex(SerializedProperty mode)
        {
            int modeIndex = 0;
            int intValue = mode.intValue;

            for (; modeIndex < modes.Length; modeIndex++)
                if (modes[modeIndex].enumValue == intValue)
                    break;

            return modeIndex;
        }

        public float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int popupIndex = GetPopupIndex(property.FindPropertyRelative(modeProperty));
            SerializedProperty choosenProperty = property.FindPropertyRelative(modes[popupIndex].propertyName);
            return EditorGUI.GetPropertyHeight(choosenProperty, label);
        }
    }
}