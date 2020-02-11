using Enderlook.Unity.Attributes.AttributeUsage;
using Enderlook.Unity.Attributes.AttributeUsage.PostCompiling.Attributes;

using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    internal static class CannotBeUsedAsMemberTesting
    {
        private static HashSet<Type> types = new HashSet<Type>();

        [ExecuteOnEachTypeWhenScriptsReloads]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper")]
        private static void GetTypes(Type type)
        {
            if (type.IsDefined(typeof(CannotBeUsedAsMemberAttribute), false))
                types.Add(type);
        }

        [ExecuteOnEachFieldOfEachTypeWhenScriptsReloads(AttributeUsage.FieldSerialization.EitherSerializableOrNotByUnity, 1)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper")]
        private static void GetFields(FieldInfo fieldInfo)
        {
            if (fieldInfo.CheckIfShouldBeIgnored(typeof(ShowIfAttribute)))
                return;
            if (types.Contains(fieldInfo.FieldType)) // We do this instead of checking if IsDefined because that method is quite slow (not tested)
                Debug.LogException(new ArgumentException($"{fieldInfo.DeclaringType} has a field {fieldInfo.Name} of type {fieldInfo.FieldType} which can not be used as member because it has defined the attribute {nameof(CannotBeUsedAsMemberAttribute)}."));
        }
    }
}