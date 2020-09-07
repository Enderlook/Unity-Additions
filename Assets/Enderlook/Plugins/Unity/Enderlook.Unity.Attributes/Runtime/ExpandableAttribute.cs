using Enderlook.Unity.Attributes.AttributeUsage;

using System;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [AttributeUsageRequireDataType(typeof(UnityEngine.Object), includeEnumerableTypes = true, typeFlags = TypeCasting.CheckSubclassOrAssignable)]
    [AttributeUsageFieldMustBeSerializableByUnity]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class ExpandableAttribute : PropertyAttribute
    {
    }
}