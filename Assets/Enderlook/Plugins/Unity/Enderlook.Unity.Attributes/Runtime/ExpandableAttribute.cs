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
        /// <summary>
        /// Whenever a button to open this field in a new windows must be shown.
        /// </summary>
        public readonly bool? showButton;

        /// <summary>
        /// Whenever the foldout content must have an outline.
        /// </summary>
        public readonly bool? isBoxed;

        /// <summary>
        /// Color multiplier of foldount content background. Lower numbers are darker.
        /// </summary>
        public readonly float? colorMultiplier;

        /// <summary>
        /// Whenever it should show the script field.
        /// </summary>
        public readonly bool? showScriptField;

        public ExpandableAttribute() { }

        public ExpandableAttribute(bool showButton) => this.showButton = showButton;

        public ExpandableAttribute(bool showButton, bool isBoxed)
        {
            this.showButton = showButton;
            this.isBoxed = isBoxed;
        }

        /// <summary>
        /// Mark the content of the decorated field to the expadable in the inspector.
        /// </summary>
        /// <param name="showButton">Whenever a button to open this field in a new windows must be shown.</param>
        /// <param name="isBoxed">Whenever the foldout content must have an outline.</param>
        /// <param name="colorMultiplier">Color multiplier of foldount content background. Lower numbers are darker.</param>
        public ExpandableAttribute(bool showButton, bool isBoxed, float colorMultiplier)
        {
            this.showButton = showButton;
            this.isBoxed = isBoxed;
            this.colorMultiplier = colorMultiplier;
        }
    }
}