using System;
using System.Collections.Generic;
using System.Text;

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

        private List<SerializedProperty> serializedProperties;
        private List<GUIContent> guiContents;
        private StringBuilder errorMessage;
        private GUIContent errorContent;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Initialize properties
            SerializedProperty minProperty = property.FindPropertyRelative(MIN_FIELD_NAME);
            SerializedProperty maxProperty = property.FindPropertyRelative(MAX_FIELD_NAME);

            if (serializedProperties is null)
                serializedProperties = new List<SerializedProperty>(3);
            else
                serializedProperties.Clear();
            serializedProperties.Add(minProperty);
            serializedProperties.Add(maxProperty);

            if (guiContents is null)
                guiContents = new List<GUIContent>();
            else
                guiContents.Clear();
            guiContents.Add(new GUIContent(MIN_FIELD_DISPLAY_NAME, minProperty.tooltip));
            guiContents.Add(new GUIContent(MAX_FIELD_DISPLAY_NAME, maxProperty.tooltip));

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

            if (errorMessage is null)
            {
                errorMessage = new StringBuilder();
                errorContent = new GUIContent();
            }
            else
                errorMessage.Clear();

            if (min >= max)
                errorMessage
                    .Append("Value of ")
                    .Append(minProperty.displayName)
                    .Append(" can't be higher or equal to ")
                    .Append(maxProperty.displayName)
                    .Append('.');
            if (step != null && step > max - min)
            {
                if (min >= max)
                    errorMessage.Append("\nAlso it");
                else
                    errorMessage
                        .Append("Value of ")
                        .Append(stepProperty.displayName);
                errorMessage
                    .Append(" can't be higher than the difference between ")
                    .Append(minProperty.displayName).Append(" and ")
                    .Append(maxProperty.displayName)
                    .Append('.');
            }
            if (errorMessage.Length > 0)
            {
                string message = errorMessage.ToString();
                Debug.LogWarning(message);
                errorContent.text = message;
                float errorHeight = GUI.skin.box.CalcHeight(errorContent, width);
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