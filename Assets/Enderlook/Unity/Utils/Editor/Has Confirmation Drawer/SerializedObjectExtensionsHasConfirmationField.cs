using Enderlook.Unity.Attributes;

using System;
using System.Linq;
using System.Reflection;

using UnityEditor;

namespace Enderlook.Unity.Utils.UnityEditor
{
    public static class SerializedObjectExtensionsHasConfirmationField
    {
        /// <summary>
        /// Generate a toggleable button to hide or show a certain field, which is also created by this method.
        /// </summary>
        /// <param name="source">Instance where its executed this method.</param>
        /// <param name="serializedProperty"Name of <see cref="SerializedProperty"/> to show in the inspector.<br>
        /// This field must have a <see cref="HasConfirmationFieldAttribute"/>.</param>
        /// <param name="includeChildren"/>If <see langword="true"/> the <paramref name="serializedProperty"/> including children is drawn.</param>
        /// <param name="bindingFlags">Binding flags used to find fields.</param>
        [Obsolete("Fields with HasConfirmationFieldAttribute do no longer require this call to show in Unity Inspector.")]
        public static void ToggleableField(this SerializedObject source, string serializedProperty, bool includeChildren = false, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            Type type = source.targetObject.GetType();
            if (!(type.GetField(serializedProperty, bindingFlags).GetCustomAttribute(typeof(HasConfirmationFieldAttribute)) is HasConfirmationFieldAttribute attribute))
                throw new Exception($"The {type}.{serializedProperty} field must have the attribute {nameof(HasConfirmationFieldAttribute)}.");
            else
                source.ToggleableField(serializedProperty, attribute.confirmFieldName, includeChildren);
        }

        /// <summary>
        /// Generate a toggleable button to hide or show all fields with <see cref="HasConfirmationFieldAttribute"/> in <see cref="SerializedObject.targetObject"/> from <paramref name="source"/>, which are also created by this method.
        /// </summary>
        /// <param name="source">Instance where its executed this method.</param>
        /// <param name="includeChildren"/>If <see langword="true"/> the <paramref name="serializedProperty"/> including children is drawn.</param>
        /// <param name="bindingFlags">Binding flags used to find fields.</param>
        [Obsolete("Fields with HasConfirmationFieldAttribute do no longer require this call to show in Unity Inspector.")]
        public static void ShowToggleableFields(this SerializedObject source, bool includeChildren = false, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            foreach (string field in HasConfirmationFieldAttribute.GetFieldsWithConfirmationAttribute(source.targetObject, bindingFlags).Select(e => e.field.Name))
                source.ToggleableField(field, includeChildren);
        }
    }
}