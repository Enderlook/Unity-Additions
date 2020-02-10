using Enderlook.Unity.Attributes.AttributeUsage;
using Enderlook.Unity.Serializables.Ranges;

using System;

using UnityEngine;

using RangeInt = Enderlook.Unity.Serializables.Ranges.RangeInt;

namespace Enderlook.Unity.Attributes
{
    [AttributeUsageRequireDataType(typeof(Vector2), typeof(Vector2Int), typeof(RangeInt), typeof(RangeIntStep), typeof(RangeFloat), typeof(RangeFloatStep), includeEnumerableTypes = true)]
    [AttributeUsageFieldMustBeSerializableByUnity]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class RangeMinMaxAttribute : RangeAttribute
    {
        public RangeMinMaxAttribute(float min, float max, float step = 0, bool showRandomButton = true) : base(min, max, step, showRandomButton) { }
    }
}