﻿using Enderlook.Reflection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Utils.UnityEditor
{
    /// <summary>
    /// A set of helper functions for <see cref="SerializedProperty"/>.
    /// </summary>
    public static class SerializedPropertyExtensions
    {
        // https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBaseEditor/EditorHelper.cs

        private static Regex isArrayRegex = new Regex(@"Array.data\[\d+\]$");

        /// <summary>
        /// Check if <paramref name="source"/> is an element from an array or list.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> to check.</param>
        /// <returns>Whenever it's an element of an array or list, or not.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static bool IsArrayOrListElement(this SerializedProperty source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return isArrayRegex.IsMatch(source.propertyPath);
        }

        /// <summary>
        /// Get the last field name of the <paramref name="source"/>.<br/>
        /// For array or list index or element it returns the field name instead of the index of the size property.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> where field name is gotten.</param>
        /// <returns>Last field name of <paramref name="source"/>.</returns>
        public static string GetFieldName(this SerializedProperty source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            string path;
            if (source.IsArrayOrListSize())
                path = source.propertyPath.Substring(0, source.propertyPath.Length - ".Array.size".Length);
            else if (source.IsArrayOrListElement())
                path = source.propertyPath.Split(new string[] { ".Array.data[" }, StringSplitOptions.None).Reverse().ElementAt(1);
            else
                path = source.propertyPath;
            return path.Split('.').Last();
        }

        /// <summary>
        /// Check if <paramref name="source"/> is the array or list size.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> to check.</param>
        /// <returns>Whenever it's the array or list size, or not</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static bool IsArrayOrListSize(this SerializedProperty source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.propertyPath.EndsWith("Array.size");
        }

        /// <summary>
        /// Gets the target object hierarchy of <paramref name="source"/>. It does work for nested serialized properties.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> whose value will be get.</param>
        /// <param name="includeItself">If <see langword="true"/> the first returned element will be <c><paramref name="source"/>.serializedObject.targetObject</c>.</param>
        /// <param name="preferNullInsteadOfException">If <see langword="true"/>, it will return <see langword="null"/> instead of throwing exceptions if can't find objects.</param>
        /// <returns>Hierarchy traveled to get the target object.</returns>
        public static IEnumerable<object> GetEnumerableTargetObjectOfProperty(this SerializedProperty source, bool includeItself = true, bool preferNullInsteadOfException = true)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Method();

            IEnumerable<object> Method()
            {
                string path = source.propertyPath.Replace(".Array.data[", "[");

                object obj = source.serializedObject.targetObject;

                if (includeItself)
                    yield return obj;

                string GetNotFoundMessage(string element) => $"The element {element} was not found in {obj.GetType()} from {source.name} in path {path}.";

                foreach (string element in path.Split('.'))
                    if (element.Contains("["))
                    {
                        string elementName = element.Substring(0, element.IndexOf("["));
                        int index = int.Parse(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                        try
                        {
                            obj = obj.GetValue(elementName, index);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            if (!preferNullInsteadOfException)
                                throw new IndexOutOfRangeException($"The element {element} has no index {index} in {obj.GetType()} from {source.name} in path {path}.", e);
                        }
                        if (obj == null && !preferNullInsteadOfException)
                            throw new KeyNotFoundException(GetNotFoundMessage(element));
                        yield return obj;
                    }
                    else
                    {
                        obj = obj.GetValue(element);
                        if (obj == null && !preferNullInsteadOfException)
                            throw new KeyNotFoundException(GetNotFoundMessage(element));
                        yield return obj;
                    }
            }
        }

        /// <summary>
        /// Gets the target object of <paramref name="source"/>. It does work for nested serialized properties.<br/>
        /// If it doesn't have parent and you look for one, it will return itself.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> whose value will be get.</param>
        /// <param name="last">At which depth from last to first should return.</param>
        /// <returns>Value of the <paramref name="source"/> as <see cref="object"/>.</returns>
        public static object GetTargetObjectOfProperty(this SerializedProperty source, int last = 0) => source.GetEnumerableTargetObjectOfProperty().Reverse().Skip(last).First();

        /// <summary>
        /// Gets the parent target object of <paramref name="source"/>. It does work for nested serialized properties.<br/>
        /// If it doesn't have parent it will return itself.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> whose value will be get.</param>
        /// <returns>Value of the <paramref name="source"/> as <see cref="object"/>.</returns>
        public static object GetParentTargetObjectOfProperty(this SerializedProperty source) => source.GetTargetObjectOfProperty(1);

        /// <summary>
        /// Get the getter and setter of <paramref name="source"/>. It does work for nested serialized properties.<br/>
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> whose getter and setter will be get.</param>
        /// <returns>Getter and setter of the <paramref name="source"/>.</returns>
        public static Accessors GetTargetObjectAccessors(this SerializedProperty source)
        {
            object parent = source.GetParentTargetObjectOfProperty();
            Type parentType = parent.GetType();

            string element = source.propertyPath.Replace(".Array.data[", "[").Split('.').Last();
            if (element.Contains("["))
            {
                string elementName = element.Substring(0, element.IndexOf("["));
                int index = int.Parse(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                Accessors accessors = new Accessors(parent, elementName, index);
                try
                {
                    accessors.Get();
                }
                catch (ArgumentOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException($"The element {element} has no index {index} in {parentType} from {source.name} in path {element}.", e);
                }
                return accessors;
            }
            else
                return new Accessors(parent, element);
        }

        /// <inheritdoc cref="GetTargetObjectAccessors(SerializedProperty)"/>
        public static Accessors<T> GetTargetObjectAccessors<T>(this SerializedProperty source)
            => new Accessors<T>(GetTargetObjectAccessors(source));

        /// <summary>
        /// Produce a <see cref="GUIContent"/> with the <see cref="SerializedProperty.displayName"/> as <see cref="GUIContent.text"/> and <see cref="SerializedProperty.tooltip"/> as <see cref="GUIContent.tooltip"/>.
        /// </summary>
        /// <param name="source">><see cref="SerializedProperty"/> to get <see cref="GUIContent"/>.</param>
        /// <returns><see cref="GUIContent"/> of <paramref name="source"/>.</returns>
        public static GUIContent GetGUIContent(this SerializedProperty source) => new GUIContent(source.displayName, source.tooltip);

        /// <summary>
        /// Produce a <see cref="GUIContent"/> with the <see cref="SerializedProperty.displayName"/> as <see cref="GUIContent.text"/> and <see cref="SerializedProperty.tooltip"/> as <see cref="GUIContent.tooltip"/>, but removing the backing field tag.
        /// </summary>
        /// <param name="source">><see cref="SerializedProperty"/> to get <see cref="GUIContent"/>.</param>
        /// <returns><see cref="GUIContent"/> of <paramref name="source"/>.</returns>
        public static GUIContent GetGUIContentOfBackingField(this SerializedProperty source)
        {
            GUIContent guiContent = source.GetGUIContent();
            guiContent.text = guiContent.text.Replace("<", "").Replace(">k__Backing Field", "");
            return guiContent;
        }

        /// <summary>
        /// Produces a <see cref="SerializedPropertyHelper"/> from <paramref name="source"/>.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> used to produce the <see cref="SerializedPropertyHelper"/>.</param>
        /// <returns><see cref="SerializedPropertyHelper"/> if <paramref name="source"/>.</returns>
        public static SerializedPropertyHelper GetHelper(this SerializedProperty source) => new SerializedPropertyHelper(source);

        /// <summary>
        /// Get the <see cref="Type"/> of the current value of <paramref name="source"/>.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> whose current <see cref="Type"/> will be get.</param>
        /// <returns><see cref="Type"/> of the current value of <paramref name="source"/>. Or <see langword="null"/> if it is empty.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0031:Use null propagation", Justification = "It could be a Unity object which doesn't support null copropagation.")]
        public static Type GetCurrentPropertyValueType(this SerializedProperty source)
        {
            object targetObject = source.GetTargetObjectOfProperty();
            return targetObject?.GetType();
        }

        /// <summary>
        /// Get the <see cref="FieldInfo"/> of <see cref="SerializedProperty"/>.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> whose <see cref="FieldInfo"/> will be get.</param>
        /// <param name="includeInheritedPrivate">Whenever it should also search private fields of supper-classes.</param>
        /// <returns><see cref="FieldInfo"/> of <paramref name="source"/>.</returns>
        public static FieldInfo GetFieldInfo(this SerializedProperty source, bool includeInheritedPrivate = true)
        {
            Type type = source.GetParentTargetObjectOfProperty().GetType();
            string name = source.GetFieldName();

            if (includeInheritedPrivate)
                return type.GetInheritedField(name, AccessorsHelper.bindingFlags);
            else
                return type.GetField(name, AccessorsHelper.bindingFlags);
        }

        /// <summary>
        /// Get the field <see cref="Type"/> of <paramref name="source"/>.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> whose <see cref="Type"/> will be get.</param>
        /// <param name="includeInheritedPrivate">Whenever it should also search private fields of supper-classes.</param>
        /// <returns><see cref="Type"/> of the <paramref name="source"/>.</returns>
        public static Type GetFieldType(this SerializedProperty source, bool includeInheritedPrivate = true)
            => source.GetFieldInfo(includeInheritedPrivate).FieldType;

        /// <summary>
        /// Get the index of the <paramref name="source"/> if it's an element of an array.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> element of array.</param>
        /// <returns>Its index.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> doesn't come from an array.</exception>
        public static int GetIndexFromArray(this SerializedProperty source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            string part = source.propertyPath.Split('.').Last().Split('[').LastOrDefault();
            if (part == default)
                throw new ArgumentException("It doesn't come from an array", nameof(source));
            else
                return int.Parse(part.Replace("]", ""));
        }

        /// <summary>
        /// Get the <see cref="SerializedProperty"/> of the backing field of a property.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> where the <see cref="SerializedProperty"/> will be taken.</param>
        /// <param name="name">Name of the property which backing field will be get.</param>
        /// <returns><see cref="SerializedProperty"/> of the backing field of <paramref name="name"/> property.</returns>
        public static SerializedProperty FindRelativeBackingFieldOfProperty(this SerializedProperty source, string name)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name.Length == 0) throw new ArgumentException("Can't be empty.", nameof(name));

            return source.FindPropertyRelative(ReflectionExtensions.GetBackingFieldName(name));
        }

        /// <summary>
        /// Get the <see cref="SerializedProperty"/> of the field or backing field of it property.
        /// </summary>
        /// <param name="source"><see cref="SerializedProperty"/> where the <see cref="SerializedProperty"/> will be taken.</param>
        /// <param name="name">Name of the property to get.</param>
        /// <returns><see cref="SerializedProperty"/> of the field or backing field of <paramref name="name"/> property.</returns>
        public static SerializedProperty FindRelativePropertyOrBackingField(this SerializedProperty source, string name)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name.Length == 0) throw new ArgumentException("Can't be empty.", nameof(name));

            SerializedProperty serializedProperty = source.FindPropertyRelative(name);
            if (serializedProperty == null)
                serializedProperty = source.FindPropertyRelative(ReflectionExtensions.GetBackingFieldName(name));
            return serializedProperty;
        }
    }
}