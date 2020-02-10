using Enderlook.Unity.Attributes.AttributeUsage;

using System;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [AttributeUsageRequireDataType(typeof(ScriptableObject), includeEnumerableTypes = true, typeFlags = TypeCasting.CheckCanBeAssignedTypes | TypeCasting.CheckIsAssignableTypes)]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class AbstractScriptableObjectAttribute : Attribute { }
}