using Enderlook.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(object), true)]
    internal class PropertyPopupDrawer2 : SmartPropertyDrawer
    {
        private static readonly Dictionary<Type, PropertyPopup> alloweds = new Dictionary<Type, PropertyPopup>();

        private static readonly HashSet<Type> disalloweds = new HashSet<Type>();

        private float height = -1;

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
        {
            Type classType = fieldInfo.FieldType;
            if (alloweds.TryGetValue(classType, out PropertyPopup propertyPopup))
                height = propertyPopup.DrawField(position, property, label);
            else if (disalloweds.Contains(classType))
                EditorGUI.PropertyField(position, property, label);
            else
            {
                PropertyPopupAttribute propertyPopupAttribute = classType.GetCustomAttribute<PropertyPopupAttribute>(true);
                if (propertyPopupAttribute == null)
                {
                    disalloweds.Add(classType);
                    EditorGUI.PropertyField(position, property, label);
                }
                else
                {
                    PropertyPopupOption[] modes =
                        classType.GetInheritedFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .Select(e => (e, e.GetCustomAttribute<PropertyPopupOptionAttribute>(true)))
                        .Where(e => e.Item2 != null)
                        .Select(e => new PropertyPopupOption(e.e.Name, e.Item2.target))
                        .ToArray();

                    propertyPopup = new PropertyPopup(propertyPopupAttribute.modeName, modes);
                    alloweds.Add(classType, propertyPopup);
                    height = propertyPopup.DrawField(position, property, label);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => height == -1 ? base.GetPropertyHeight(property, label) : height;
    }
}
