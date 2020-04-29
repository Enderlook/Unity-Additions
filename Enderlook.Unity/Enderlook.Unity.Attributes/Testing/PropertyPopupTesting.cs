using Enderlook.Extensions;
using Enderlook.Unity.Attributes.AttributeUsage;
using Enderlook.Unity.Attributes.AttributeUsage.PostCompiling;
using Enderlook.Unity.Attributes.AttributeUsage.PostCompiling.Attributes;

using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    internal class PropertyPopupTesting
    {
        private static readonly Dictionary<Type, PropertyPopupAttribute> typesAndAttributes = new Dictionary<Type, PropertyPopupAttribute>();

        private static readonly Dictionary<Type, List<FieldInfo>> typesAndFieldAttributes = new Dictionary<Type, List<FieldInfo>>();

        [ExecuteOnEachTypeWhenScriptsReloads(ExecuteOnEachTypeWhenScriptsReloads.TypeFlags.IsNonEnum, 0)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper")]
        private static void GetTypes(Type type)
        {
            if (type.CheckIfShouldBeIgnored(typeof(PropertyPopupAttribute)))
                return;
            if (type.GetCustomAttribute<PropertyPopupAttribute>() is PropertyPopupAttribute attribute)
            {
                FieldInfo fieldInfo = type.GetInheritedField(attribute.modeName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (fieldInfo == null)
                    Debug.LogError($"Type {type} has attribute {nameof(PropertyPopupAttribute)}, but doesn't have a field named {attribute.modeName} as {nameof(PropertyPopupAttribute.modeName)} requires nor its bases classes have it.");
                else if (fieldInfo.CanBeSerializedByUnity() && !fieldInfo.CheckIfShouldBeIgnored(typeof(PropertyPopupAttribute)))
                    Debug.LogError($"Type {type} has attribute {nameof(PropertyPopupAttribute)} which uses the field {fieldInfo.Name} declared in {fieldInfo.DeclaringType} as mode field, but it's no serializable by Unity.");
            }
        }

        [ExecuteOnEachFieldOfEachTypeWhenScriptsReloads(FieldSerialization.SerializableByUnity, 0)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper")]
        private static void GetFields(FieldInfo fieldInfo)
        {
            if (fieldInfo.CheckIfShouldBeIgnored(typeof(PropertyPopupOptionAttribute)))
                return;
            if (fieldInfo.GetCustomAttribute<PropertyPopupOptionAttribute>() is PropertyPopupOptionAttribute attribute)
            {
                Type type = fieldInfo.DeclaringType;
                if (typesAndFieldAttributes.TryGetValue(type, out List<FieldInfo> list))
                    list.Add(fieldInfo);
                else
                    typesAndFieldAttributes.Add(type, new List<FieldInfo>() { fieldInfo });
            }
        }

        [ExecuteWhenScriptsReloads(1)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper")]
        private static void CheckFields()
        {
            foreach (KeyValuePair<Type, List<FieldInfo>> kv in typesAndFieldAttributes)
            {
                if (!typesAndAttributes.ContainsKey(kv.Key))
                    foreach (FieldInfo field in kv.Value)
                        Debug.LogError($"Type {kv.Key} nor its base classes have attribute {nameof(PropertyPopupAttribute)}, but its field named {field.Name} has the attribute {nameof(PropertyPopupOptionAttribute)}.");
                foreach (FieldInfo fieldInfo in kv.Value)
                    if (!fieldInfo.CanBeSerializedByUnity())
                        Debug.LogError($"Type {kv.Key} has a field named {fieldInfo.Name} with the attribute {nameof(PropertyPopupAttribute)}, but it's not serializable by unity.");
            }
        }
    }
}
