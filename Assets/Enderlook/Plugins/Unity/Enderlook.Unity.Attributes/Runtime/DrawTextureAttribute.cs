using Enderlook.Unity.Attributes.AttributeUsage;

using System;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [AttributeUsageRequireDataType(typeof(Sprite), typeof(Texture2D), includeEnumerableTypes = true)]
    [AttributeUsageFieldMustBeSerializableByUnity]
    public class DrawTextureAttribute : PropertyAttribute
    {
        [Flags]
        public enum Hide
        {
            /// <summary>
            /// The texture will be shown along with the label and field.
            /// </summary>
            None = 0,

            /// <summary>
            /// The label will be hide.
            /// </summary>
            Label = 1 << 0,

            /// <summary>
            /// The field will be hide.
            /// </summary>
            Field = 1 << 1,

            /// <summary>
            /// Both <see cref="Hide.Label"/> and <see cref="Hide.Field"/>.
            /// </summary>
            All = Label | Field,
        }

        /// <summary>
        /// Whenever the texture will be drawn on the same line as the property or in a line bellow.<br/>
        /// This is ignored if <see cref="hideMode"/> is <see cref="Hide.All"/>.
        /// </summary>
        public readonly bool drawOnSameLine;

        /// <summary>
        /// Whenever the textre will be centered.<br/>
        /// This is ignored if <see cref="drawOnSameLine"/> is <see langword="true"/>.
        /// </summary>
        public readonly bool centered;

        /// <summary>
        /// Height of the <see cref="Rect"/> used to show the texture.<br/>
        /// On -1, the height of the property is used.
        /// </summary>
        public readonly float height;

        /// <summary>
        /// Width of the <see cref="Rect"/> used to show the texture.<br/>
        /// On -1, <see cref="height"/> is used.
        /// </summary>
        public readonly float width;

        /// <summary>
        /// Determines how it will be shown in the inspector.
        /// </summary>
        public readonly Hide hideMode;

        /// <summary>
        /// Draw the texture next to the field in the inspector.
        /// </summary>
        /// <param name="drawOnSameLine">Whenever the texture will be drawn on the same line as the property or in a line bellow.</param>
        /// <param name="centered">Whenever the textre will be centered.<br/>
        /// This is ignored if <paramref name="drawOnSameLine"/> is <see langword="true"/>.</param>
        public DrawTextureAttribute(bool drawOnSameLine = true, bool centered = false) : this(Hide.None, -1, -1, drawOnSameLine, centered) { }

        /// <summary>
        /// Draw the texture below the field in the inspector.
        /// </summary>
        /// <param name="size">Size of the <see cref="Rect"/> used to show the texture.<br/>
        /// On -1, the height of the property is used.</param>
        /// <param name="drawOnSameLine">Whenever the texture will be drawn on the same line as the property or in a line bellow.</param>
        /// <param name="centered">Whenever the textre will be centered.<br/>
        /// This is ignored if <paramref name="drawOnSameLine"/> is <see langword="true"/>.</param>
        public DrawTextureAttribute(float size, bool drawOnSameLine = false, bool centered = false) : this(Hide.None, size, size, drawOnSameLine, centered) { }

        /// <summary>
        /// Draw the texture below the field in the inspector.
        /// </summary>
        /// <param name="height">Height of the <see cref="Rect"/> used to show the texture.<br/>
        /// On -1, the height of the property is used.</param>
        /// <param name="width">Width of the <see cref="Rect"/> used to show the texture.<br/>
        /// On -1, <paramref name="height"/> is used.</param>
        /// <param name="drawOnSameLine">Whenever the texture will be drawn on the same line as the property or in a line bellow.</param>
        /// <param name="centered">Whenever the textre will be centered.<br/>
        /// This is ignored if <paramref name="drawOnSameLine"/> is <see langword="true"/>.</param>
        public DrawTextureAttribute(float height, float width, bool drawOnSameLine = false, bool centered = false) : this(Hide.None, height, width, drawOnSameLine, centered) { }

        /// <summary>
        /// Draw the texture of the field in the inspector.
        /// </summary>
        /// <param name="hideMode">Configure parts of the property that won't be shown.</param>
        /// <param name="size">Size of the <see cref="Rect"/> used to show the texture.<br/>
        /// On -1, the height of the property is used.</param>
        /// <param name="drawOnSameLine">Whenever the texture will be drawn on the same line as the property or in a line bellow.<br/>
        /// This is ignored if <see cref="hideMode"/> is <see cref="Hide.All"/>.</param>
        /// <param name="centered">Whenever the textre will be centered.<br/>
        /// This is ignored if <paramref name="drawOnSameLine"/> is <see langword="true"/>.</param>
        public DrawTextureAttribute(Hide hideMode, float size, bool drawOnSameLine = false, bool centered = false) : this(hideMode, size, size, drawOnSameLine, centered) { }

        /// <summary>
        /// Draw the texture of the field in the inspector.
        /// </summary>
        /// <param name="hideMode">Configure parts of the property that won't be shown.</param>
        /// <param name="height">Height of the <see cref="Rect"/> used to show the texture.<br/>
        /// On -1, the height of the property is used.</param>
        /// <param name="width">Width of the <see cref="Rect"/> used to show the texture.<br/>
        /// On -1, <paramref name="height"/> is used.</param>
        /// <param name="drawOnSameLine">Whenever the texture will be drawn on the same line as the property or in a line bellow.<br/>
        /// This is ignored if <see cref="hideMode"/> is <see cref="Hide.All"/>.</param>
        /// <param name="centered">Whenever the textre will be centered.<br/>
        /// This is ignored if <paramref name="drawOnSameLine"/> is <see langword="true"/>.</param>
        public DrawTextureAttribute(Hide hideMode, float height, float width, bool drawOnSameLine = false, bool centered = false)
        {
            this.hideMode = hideMode;
            this.height = height;
            this.width = width;
            this.drawOnSameLine = drawOnSameLine;
            this.centered = centered;
        }
    }
}