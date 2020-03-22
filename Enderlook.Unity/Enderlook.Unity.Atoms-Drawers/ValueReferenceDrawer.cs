using Enderlook.Unity.Attributes;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    [CustomPropertyDrawer(typeof(BaseValueReference), true)]
    internal class ValueReferenceDrawer : SmartPropertyDrawer
    {
        private static ReferenceDrawer referenceDrawer = new ReferenceDrawer(
            ("Inline", "inline", (int)BaseValueReference.ReferenceMode.Inline),
            ("ScriptableObject", "scriptableObject", (int)BaseValueReference.ReferenceMode.ScriptableObject),
            ("Component", "component", (int)BaseValueReference.ReferenceMode.Component)
        );

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
            => referenceDrawer.OnGUI(position, property, label);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => referenceDrawer.GetPropertyHeight(property, label);
    }
}