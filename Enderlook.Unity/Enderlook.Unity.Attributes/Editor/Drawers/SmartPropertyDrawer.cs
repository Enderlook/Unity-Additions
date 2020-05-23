using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    public abstract class SmartPropertyDrawer : SmartBasePropertyDrawer
    {
        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Before(ref position, ref property, ref label);
            OnGUISmart(position, property, label);
            After(position, property, label);
        }

        protected abstract void OnGUISmart(Rect position, SerializedProperty property, GUIContent label);
    }
}