using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    public class PropertyDrawerInfo
    {
        public SerializedProperty Property { get; private set; }

        public Rect Position { get; private set; }

        public GUIContent Label { get; private set; }

        public FieldInfo FieldInfo { get; private set; }

        public Type Type => PropertyWrapper.GetType(Property, FieldInfo);

        public PropertyDrawerInfo(FieldInfo fieldInfo, SerializedProperty property, GUIContent label, Rect position)
        {
            FieldInfo = fieldInfo;
            Property = property;
            Label = label;
            Position = position;
        }
    }
}