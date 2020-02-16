﻿using Enderlook.Unity.Attributes.AttributeUsage.PostCompiling.Attributes;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Enderlook.Unity.Attributes.AttributeUsage
{
    internal static class AttributeUsageRequireDataTypeTesting
    {
        private static Dictionary<Type, (AttributeTargets targets, Action<Type, string> checker)> checkers = new Dictionary<Type, (AttributeTargets targets, Action<Type, string> checker)>();

        [ExecuteOnEachTypeWhenScriptsReloads(ExecuteOnEachTypeWhenScriptsReloads.TypeFlags.IsNonEnum, 0)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper.")]
        private static void GetAttributesAndTypes(Type type)
        {
            if (type.IsSubclassOf(typeof(Attribute)) && type.GetCustomAttribute<AttributeUsageRequireDataTypeAttribute>(true) is AttributeUsageRequireDataTypeAttribute attribute)
            {
                AttributeUsageAttribute attributeUsageAttribute = type.GetCustomAttribute<AttributeUsageAttribute>();
                checkers.Add(type, (attributeUsageAttribute?.ValidOn ?? AttributeTargets.All, (checkType, checkName) => attribute.CheckAllowance(checkType, checkName, type.Name)));
            }
        }

        [ExecuteOnEachTypeWhenScriptsReloads(ExecuteOnEachTypeWhenScriptsReloads.TypeFlags.IsNonEnum, 1)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper.")]
        private static void CheckClasses(Type type)
        {
            if (type.CheckIfShouldBeIgnored(typeof(AttributeUsageMethodAttribute)))
                return;
            foreach (Attribute attribute in type.GetCustomAttributes())
            {
                Type attributeType = attribute.GetType();
                if (checkers.TryGetValue(attributeType, out (AttributeTargets targets, Action<Type, string> checker) value)
                    && (value.targets & AttributeTargets.Class) != 0 // Check if has the proper flag
                    && type.CheckIfShouldBeIgnored(attributeType))
                    value.checker(type, $"Class {type.Name}");
            }
        }

        private static void CheckSomething(MemberInfo memberInfo, Type type, string memberType, AttributeTargets checkIf)
        {
            foreach (Attribute attribute in memberInfo.GetCustomAttributes())
            {
                Type attributeType = attribute.GetType();
                if (checkers.TryGetValue(attributeType, out (AttributeTargets targets, Action<Type, string> checker) value)
                    && (value.targets & checkIf) != 0 // Check if has the proper flag
                    && !memberInfo.CheckIfShouldBeIgnored(attributeType))
                    value.checker(type, $"{memberType} {memberInfo.Name} in {memberInfo.DeclaringType.Name} class");
            }
        }

        [ExecuteOnEachFieldOfEachTypeWhenScriptsReloads(FieldSerialization.EitherSerializableOrNotByUnity, 1)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper.")]
        private static void CheckFields(FieldInfo fieldInfo) => CheckSomething(fieldInfo, fieldInfo.FieldType, "Field", AttributeTargets.Field);

        [ExecuteOnEachPropertyOfEachTypeWhenScriptsReloads(1)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper.")]
        private static void CheckProperties(PropertyInfo propertyInfo) => CheckSomething(propertyInfo, propertyInfo.PropertyType, "Property", AttributeTargets.Property);

        [ExecuteOnEachMethodOfEachTypeWhenScriptsReloads(1)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by PostCompilingAssembliesHelper.")]
        private static void CheckMethodReturns(MethodInfo methodInfo) => CheckSomething(methodInfo, methodInfo.ReturnType, "Method return", AttributeTargets.Method);
    }
}
