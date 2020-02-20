using System;

namespace Enderlook.Unity.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeUsedAsMemberAttribute : Attribute { }
}