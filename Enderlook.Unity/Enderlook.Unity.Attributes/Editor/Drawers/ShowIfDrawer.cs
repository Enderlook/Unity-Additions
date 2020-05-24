using Enderlook.Extensions;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    internal class ShowIfDrawer : SmartPropertyDrawer
    {
        /// <summary>
        /// If <see langword="true"/>, the property field is either disabled or hidden.
        /// </summary>
        private bool? active;

        private ShowIfAttribute.ActionMode mode;

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIfAttribute = (ShowIfAttribute)attribute;
            mode = showIfAttribute.mode;

            if (IsActive)
            {
                if (mode == ShowIfAttribute.ActionMode.ShowHide)
                    DrawField();
                else if (mode == ShowIfAttribute.ActionMode.EnableDisable)
                {
                    EditorGUI.BeginDisabledGroup(active.Value);
                    DrawField();
                    EditorGUI.EndDisabledGroup();
                }
            }

            void DrawField()
            {
                SerializedPropertyGUIHelper.GetGUIContent(helper, ref label);
                EditorGUI.PropertyField(position, property, label, true);
            }

            // Force re-checking on next draw
            active = null;
        }

        protected override float GetPropertyHeightSmart(SerializedProperty property, GUIContent label)
            => mode == ShowIfAttribute.ActionMode.EnableDisable || IsActive ?
                EditorGUI.GetPropertyHeight(property, label, true)
                : 0;

        private bool IsActive {
            get {
                if (active.HasValue)
                    return active.Value;

                ShowIfAttribute showIfAttribute = (ShowIfAttribute)attribute;
                mode = showIfAttribute.mode;

                if (helper.TryGetParentTargetObjectOfProperty(out object parent))
                {
                    active = parent.GetValueFromFirstMember<bool>(showIfAttribute.nameOfConditional);
                    active = active == showIfAttribute.goal;
                    return active.Value;
                }

                return false;
            }
        }
    }
}