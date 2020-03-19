using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    public abstract class SmartPropertyDrawer : SmartBasePropertyDrawer
    {
        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize(ref position, ref property, ref label);
            OnGUISmart(position, property, label);
        }

        protected abstract void OnGUISmart(Rect position, SerializedProperty property, GUIContent label);
    }
}