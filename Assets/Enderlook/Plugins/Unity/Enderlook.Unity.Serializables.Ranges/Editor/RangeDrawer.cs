using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Ranges
{
    // TODO: This should be internal
    [CustomPropertyDrawer(typeof(RangeFloat), true), CustomPropertyDrawer(typeof(RangeInt), true), CustomPropertyDrawer(typeof(RangeFloatStep), true), CustomPropertyDrawer(typeof(RangeIntStep), true)]
    public class RangeDrawer : PropertyDrawer
    {
        // Field display name must be a single letter
        // Public consts are used in MinMaxRangeAttributeDrawer
        public const string MIN_FIELD_NAME = "min", MAX_FIELD_NAME = "max", STEP_FIELD_NAME = "step";
        public const string STEP_FIELD_DISPLAY_NAME = "S";
        private const string MIN_FIELD_DISPLAY_NAME = "L", MAX_FIELD_DISPLAY_NAME = "U";

        protected readonly string SERIALIZED_PROPERTY_TYPE_ERROR = $"Serialized properties shown in {typeof(RangeDrawer)} must be either {nameof(SerializedPropertyType.Float)} or {nameof(SerializedPropertyType.Integer)}";

        private const int SPACE_BETTWEN_FIELD_AND_ERROR = 2;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Initialize properties
            SerializedProperty minProperty = property.FindPropertyRelative(MIN_FIELD_NAME);
            SerializedProperty maxProperty = property.FindPropertyRelative(MAX_FIELD_NAME);

            List<SerializedProperty> serializedProperties = new List<SerializedProperty>()
            {
                minProperty,
                maxProperty,
            };

            List<GUIContent> guiContents = new List<GUIContent>()
            {
                new GUIContent(MIN_FIELD_DISPLAY_NAME, minProperty.tooltip),
                new GUIContent(MAX_FIELD_DISPLAY_NAME, maxProperty.tooltip),
            };

            SerializedProperty stepProperty = property.FindPropertyRelative(STEP_FIELD_NAME);
            if (stepProperty != null)
            {
                serializedProperties.Add(stepProperty);
                guiContents.Add(new GUIContent(STEP_FIELD_DISPLAY_NAME, stepProperty.tooltip));
            }

            // This magically iterate over all sibling of the serialized property and show them in-line in the inspector
            // The serialized property is the first in being shown
            EditorGUI.MultiPropertyField(position, guiContents.ToArray(), property.FindPropertyRelative(MIN_FIELD_NAME), label);

            (float errorHeight, string message) = CalculateError(position.width, minProperty, maxProperty, stepProperty);
            if (errorHeight != 0)
                EditorGUI.HelpBox(new Rect(position.x, position.y + position.height - errorHeight + SPACE_BETTWEN_FIELD_AND_ERROR, position.width, errorHeight), message, MessageType.Error);
        }

        private (float errorHeight, string message) CalculateError(float width, SerializedProperty minProperty, SerializedProperty maxProperty, SerializedProperty stepProperty)
        {
            // Validate fields
            float min, max;
            float? step = null;
            // All properties has the same type, so check the first one
            switch (minProperty.propertyType)
            {
                case SerializedPropertyType.Float:
                    min = minProperty.floatValue;
                    max = maxProperty.floatValue;
                    if (stepProperty != null)
                        step = stepProperty.floatValue;
                    break;
                case SerializedPropertyType.Integer:
                    min = minProperty.intValue;
                    max = maxProperty.intValue;
                    if (stepProperty != null)
                        step = stepProperty.intValue;
                    break;
                default:
                    throw new ArgumentException(SERIALIZED_PROPERTY_TYPE_ERROR);
            }

            List<string> errors = new List<string>();
            if (min >= max)
                errors.Add($"Value of {minProperty.displayName} can't be higher or equal to {maxProperty.displayName}.");
            if (step != null && step > max - min)
                errors.Add($"Value of {stepProperty.displayName} can't be higher than the difference between {minProperty.displayName} and {maxProperty.displayName}.");
            if (errors.Count > 0)
            {
                foreach (string error in errors)
                    Debug.LogWarning(error);
                string message = string.Join("\n", errors);
                float errorHeight = GUI.skin.box.CalcHeight(new GUIContent(message), width);
                return (errorHeight, message);
            }
            return (0, null);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty minProperty = property.FindPropertyRelative(MIN_FIELD_NAME);
            SerializedProperty maxProperty = property.FindPropertyRelative(MAX_FIELD_NAME);
            SerializedProperty stepProperty = property.FindPropertyRelative(STEP_FIELD_NAME);

            float height = EditorGUI.GetPropertyHeight(property, label);

            float errorHeight = CalculateError(EditorGUIUtility.currentViewWidth, minProperty, maxProperty, stepProperty).errorHeight;
            if (errorHeight != 0)
                height += errorHeight + SPACE_BETTWEN_FIELD_AND_ERROR;

            if (!EditorGUIUtility.wideMode)
                height += EditorGUIUtility.singleLineHeight;

            return height;
        }
    }
}