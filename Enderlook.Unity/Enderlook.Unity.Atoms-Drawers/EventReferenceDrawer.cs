using Enderlook.Unity.Attributes;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    [CustomPropertyDrawer(typeof(BaseEventReference), true)]
    internal class EventReferenceDrawer : SmartPropertyDrawer
    {
        private static ReferenceDrawer referenceDrawer = new ReferenceDrawer(
            ("Event", "event", (int)BaseEventReference.ReferenceMode.Event),
            ("Managed Scriptable Object", "managedScriptableObject", (int)BaseEventReference.ReferenceMode.ManagedScriptableObject),
            ("Managed Component", "managedComponent", (int)BaseEventReference.ReferenceMode.ManagedComponent)
        );

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
            => referenceDrawer.OnGUI(position, property, label);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => referenceDrawer.GetPropertyHeight(property, label);
    }
}