using Enderlook.Unity.Attributes.AttributeUsage;
using Enderlook.Unity.Attributes.AttributeUsage.PostCompiling;
using Enderlook.Unity.Attributes.AttributeUsage.PostCompiling.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    internal static class HasConfirmationFieldTesting
    {
        private static readonly Dictionary<Type, List<HasConfirmationFieldAttribute>> typesAndAttributes = new Dictionary<Type, List<HasConfirmationFieldAttribute>>();

        [ExecuteOnEachFieldOfEachTypeWhenScriptsReloads(FieldSerialization.SerializableByUnity, 0)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper")]
        private static void GetFields(FieldInfo fieldInfo)
        {
            if (fieldInfo.GetCustomAttribute<HasConfirmationFieldAttribute>() is HasConfirmationFieldAttribute attribute)
            {
                Type type = fieldInfo.DeclaringType;
                if (typesAndAttributes.TryGetValue(type, out List<HasConfirmationFieldAttribute> list))
                    list.Add(attribute);
                else
                    typesAndAttributes.Add(type, new List<HasConfirmationFieldAttribute>() { attribute });
            }
        }

        private const string ERROR_MISSING_FIELD_MESSAGE = "{0} does not have a field of type " + nameof(Boolean) + " named {1} necessary for attribute " + nameof(HasConfirmationFieldAttribute) + ".";

        [ExecuteWhenScriptsReloads(1)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper")]
        private static void CheckFields()
        {
            foreach (KeyValuePair<Type, List<HasConfirmationFieldAttribute>> classToCheck in typesAndAttributes)
            {
                Type classType = classToCheck.Key;
                HashSet<string> confirmFields = new HashSet<string>(classToCheck.Value.Select(e => e.confirmFieldName));

                confirmFields.ExceptWith(new HashSet<string>(
                    classType
                        .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                        .Where(e => e.FieldType == typeof(bool) && e.CanBeSerializedByUnity())
                        .Select(e => e.Name)
                    )
                );

                foreach (string field in confirmFields)
                    Debug.LogError(string.Format(ERROR_MISSING_FIELD_MESSAGE, classType, field));
            }
        }
    }
}