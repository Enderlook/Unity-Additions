﻿using System;

namespace Enderlook.Unity.Attributes.AttributeUsage
{
    [AttributeUsageRequireDataType(typeof(Attribute), typeFlags = TypeCasting.CheckSubclassTypes)]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class AttributeUsageFieldMustBeSerializableByUnityAttribute : Attribute { }
}