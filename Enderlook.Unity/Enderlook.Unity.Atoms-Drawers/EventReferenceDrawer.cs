using Enderlook.Extensions;
using Enderlook.Unity.Attributes;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    [CustomPropertyDrawer(typeof(BaseEventReference), true)]
    internal class EventReferenceDrawer : SmartPropertyDrawer
    {
        private static PropertyPopup referenceDrawer = new PropertyPopup(
            ReflectionExtensions.GetBackingFieldName("Mode"),
            ("Event", "event", (int)BaseEventReference.ReferenceMode.Event),
            ("Managed Scriptable Object", "managedScriptableObject", (int)BaseEventReference.ReferenceMode.ManagedScriptableObject),
            ("Managed Component", "managedComponent", (int)BaseEventReference.ReferenceMode.ManagedComponent)
        );

        private float height;

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
            => height = referenceDrawer.DrawField(position, property, label);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => height;
    }
}