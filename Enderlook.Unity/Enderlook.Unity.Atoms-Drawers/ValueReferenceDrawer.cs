using Enderlook.Extensions;
using Enderlook.Unity.Attributes;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    [CustomPropertyDrawer(typeof(BaseValueReference), true)]
    internal class ValueReferenceDrawer : SmartPropertyDrawer
    {
        private static PropertyPopup referenceDrawer = new PropertyPopup(
            ReflectionExtensions.GetBackingFieldName("Mode"),
            ("Inline", "inline", (int)BaseValueReference.ReferenceMode.Inline),
            ("ScriptableObject", "scriptableObject", (int)BaseValueReference.ReferenceMode.ScriptableObject),
            ("Component", "component", (int)BaseValueReference.ReferenceMode.Component)
        );

        private float height;

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
            => height = referenceDrawer.DrawField(position, property, label);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => height;
    }
}