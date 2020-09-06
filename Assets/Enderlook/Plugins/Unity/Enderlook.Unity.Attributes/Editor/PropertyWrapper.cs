using Enderlook.Unity.Utils.UnityEditor;

using System;
using System.Reflection;

using UnityEditor;

using UnityObject = UnityEngine.Object;

namespace Enderlook.Unity.Attributes
{
    internal class PropertyWrapper
    {
        public SerializedProperty Property { get; }

        public FieldInfo FieldInfo { get; }

        public Func<object> Get { get; }

        public Action<object> Set { get; }

        public Type Type { get; }

        public PropertyWrapper(SerializedProperty property, FieldInfo fieldInfo)
        {
            Property = property;
            FieldInfo = fieldInfo;

            /* If the property came from an array and the element is null this will be null which is a problem for us.
             * This is also null if the property isn't array but the field is empty (null). That is also a problem. */
            if (property.objectReferenceValue)
            {
                (Get, Set) = property.GetTargetObjectAccessors();
                Type = property.GetFieldType();
            }
            else
            {
                UnityObject targetObject = property.serializedObject.targetObject;
                Type fieldType = fieldInfo.FieldType;
                // Just confirming that it's an array
                if (fieldType.IsArray)
                {
                    Type = fieldType.GetElementType();
                    int index = property.GetIndexFromArray();

                    if (fieldInfo.GetValue(targetObject) is Array array)
                    {
                        /* Until an element is in-Inspector dragged to the array element field, it seems that Unity doesn't rebound the array
                         * So if the array is empty and it doesn't have space for us, we make a new array and inject it. */
                        if (array.Length == 0)
                        {
                            array = Array.CreateInstance(fieldType.GetElementType(), 1);
                            fieldInfo.SetValue(targetObject, array);
                        }

                        Get = () => array.GetValue(index);
                        Set = (value) => array.SetValue(value, index);
                    }
                    else
                        throw new InvalidCastException();
                }
                else
                {
                    Type = fieldType;
                    Get = () => property.objectReferenceValue;
                    Action<object, object> set2;
                    if (fieldInfo.FieldType == targetObject.GetType())
                        set2 = fieldInfo.SetValue;
                    else
                    {
                        FieldInfo fieldInfo2 = targetObject
                                .GetType()
                                .GetField(property.name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (fieldInfo2 != null)
                            set2 = fieldInfo2.SetValue;
                        else
                            set2 = (_, value) => property.objectReferenceValue = (UnityObject)value;
                    }
                    Set = (value) => set2(targetObject, value);
                }
            }
        }

        public static Type GetType(SerializedProperty property, FieldInfo fieldInfo)
        {
            if (property.objectReferenceValue)
                return property.GetFieldType();
            else
            {
                Type fieldType = fieldInfo.FieldType;
                if (fieldType.IsArray)
                    return fieldType.GetElementType();
                else
                    return fieldType;
            }
        }

        public void ApplyModifiedProperties() => Property.serializedObject.ApplyModifiedProperties();
    }
}
