﻿using Enderlook.Extensions.code;
using Enderlook.Unity.Utils.UnityEditor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    /// <summary>
    /// A helper class to draw properties according to a popup selector.
    /// </summary>
    public class PropertyPopup
    {
        private readonly static GUIStyle popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
        {
            imagePosition = ImagePosition.ImageOnly
        };

        private readonly string modeProperty;
        private readonly (string displayName, string propertyName, object target)[] modes;
        private readonly string[] popupOptions;

        /// <summary>
        /// Determie the posible options for the popup.
        /// </summary>
        /// <param name="modeProperty">Property used to determine which property draw.</param>
        /// <param name="modes">Possible options. (name to show in inspector, name of property which must show if selected, target value to determine if chosen).</param>
        public PropertyPopup(string modeProperty, params (string displayName, string propertyName, object target)[] modes)
        {
            this.modeProperty = modeProperty;
            this.modes = modes;
            popupOptions = modes.Select(e => e.displayName).ToArray();
        }

        /// <summary>
        /// Draw the field in the given place.
        /// </summary>
        /// <param name="position">Position to draw the field.</param>
        /// <param name="property">Property used to draw the field.</param>
        /// <param name="label">Label to show in inspector.</param>
        /// <returns>Property height.</returns>
        public float DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            (SerializedProperty mode, int index) = GetModeAndIndex(property);
            OnGUI(position, property, label, mode, index);
            return GetPropertyHeight(property, label, index);
        }

        /// <summary>
        /// Draw the field in the given place.
        /// </summary>
        /// <param name="position">Position to draw the field.</param>
        /// <param name="property">Property used to draw the field.</param>
        /// <param name="label">Label to show in inspector.</param>
        public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            (SerializedProperty mode, int index) = GetModeAndIndex(property);
            OnGUI(position, property, label, mode, index);
        }

        private void OnGUI(Rect position, SerializedProperty property, GUIContent label, SerializedProperty mode, int popupIndex)
        {
            // Show field label
            position = EditorGUI.PrefixLabel(position, label);

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(
                position.x,
                position.y + popupStyle.margin.top,
                popupStyle.fixedWidth + popupStyle.margin.right,
                position.height - popupStyle.margin.top);
            position.xMin = buttonRect.xMax;

            int newUsagePopupIndex = EditorGUI.Popup(buttonRect, popupIndex, popupOptions, popupStyle);
            if (newUsagePopupIndex != popupIndex)
            {
                object parent = mode.GetParentTargetObjectOfProperty();
                object value = modes[newUsagePopupIndex].target;
                FieldInfo fieldInfo = mode.GetFieldInfo();
                Type fieldType = fieldInfo.FieldType;
                if (fieldType.IsEnum)
                    fieldInfo.SetValue(parent, Enum.ToObject(fieldType, value));
                else
                    fieldInfo.SetValue(parent, value);
                mode.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.PropertyField(position,
                property.FindPropertyRelative(modes[newUsagePopupIndex].propertyName),
                GUIContent.none);
        }

        private (SerializedProperty mode, int index) GetModeAndIndex(SerializedProperty property)
        {
            // Get current mode
            SerializedProperty mode = property.FindPropertyRelative(modeProperty);
            if (mode == null)
                throw new ArgumentNullException(nameof(mode), $"Can't find propety {mode.name} at path {mode.propertyPath} in {property.name}.");
            int popupIndex = GetPopupIndex(mode);
            return (mode, popupIndex);
        }

        private int GetPopupIndex(SerializedProperty mode)
        {
            int modeIndex = 0;
            object value = mode.GetTargetObjectOfProperty();

            // We give special treat with enums
            Type type = value.GetType();
            if (value != null && type.IsEnum)
                value = ((Enum)value).GetUnderlyingValue();

            for (; modeIndex < modes.Length; modeIndex++)
                if (modes[modeIndex].target.Equals(value))
                    return modeIndex;

            throw new KeyNotFoundException($"Not found an option which satisfy {mode.propertyPath} ({value}).");
        }

        /// <summary>
        /// Get the height of the drawed property
        /// </summary>
        /// <param name="property">Property used to draw the field.</param>
        /// <param name="label">Label to show in inspector.</param>
        /// <returns>Property height.</returns>
        public float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int popupIndex = GetPopupIndex(property.FindPropertyRelative(modeProperty));
            return GetPropertyHeight(property, label, popupIndex);
        }

        private float GetPropertyHeight(SerializedProperty property, GUIContent label, int popupIndex)
        {
            SerializedProperty choosenProperty = property.FindPropertyRelative(modes[popupIndex].propertyName);
            return EditorGUI.GetPropertyHeight(choosenProperty, label);
        }
    }
}