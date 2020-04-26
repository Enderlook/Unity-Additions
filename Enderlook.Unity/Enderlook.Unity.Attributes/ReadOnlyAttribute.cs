using Enderlook.Unity.Attributes.AttributeUsage;

using System;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    [AttributeUsageFieldMustBeSerializableByUnity]
    public sealed class ReadOnlyAttribute : PropertyAttribute { }
}