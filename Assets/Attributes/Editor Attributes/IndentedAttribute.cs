﻿using Additions.Attributes.AttributeUsage;

using System;

using UnityEngine;

namespace Additions.Attributes
{
    /// <summary>
    /// Add or remove indentation to the drew serialized property.
    /// </summary>
    [AttributeUsageFieldMustBeSerializableByUnityAttribute]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class IndentedAttribute : PropertyAttribute
    {
        /// <summary>
        /// Indentation to add.
        /// </summary>
        public readonly int indentationOffset;

        /// <summary>
        /// Add or remove indentation to the drew serialized property.
        /// </summary>
        /// <param name="indentationOffset">Indentation to add. Negative values remove indentation.</param>
        public IndentedAttribute(int indentationOffset = 1) => this.indentationOffset = indentationOffset;
    }
}