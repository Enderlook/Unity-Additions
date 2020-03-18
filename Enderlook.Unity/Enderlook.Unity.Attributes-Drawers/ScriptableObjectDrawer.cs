using UnityEditor;
using UnityEditor.Callbacks;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    // We require this dummy drawer in order to active the additional property drawer required by the context menu

    [CustomPropertyDrawer(typeof(ScriptableObject), true)]
    internal class ScriptableObjectDrawer : AdditionalPropertyDrawer
    {
        [DidReloadScripts]
        private static void SuscribeContextMenu()
        {
            AddContextMenuItemGenerator((PropertyDrawerInfo propertyDrawerInfo, out bool valid, out GUIContent guiContent, out GenericMenu.MenuFunction menuFunction) =>
            {
                if (typeof(ScriptableObject).IsAssignableFrom(propertyDrawerInfo.Type))
                {
                    valid = true;
                    guiContent = new GUIContent("Scriptable Object Menu", "Open the Scriptable Object Menu.");
                    menuFunction = () => ScriptableObjectWindow.CreateWindow(propertyDrawerInfo.Property, propertyDrawerInfo.FieldInfo);
                    return true;
                }
                valid = false;
                guiContent = null;
                menuFunction = null;
                return false;
            });
        }

        protected override void OnGUIAdditional(Rect position, SerializedProperty property, GUIContent label)
            => EditorGUI.PropertyField(position, property, label, true);
    }
}
