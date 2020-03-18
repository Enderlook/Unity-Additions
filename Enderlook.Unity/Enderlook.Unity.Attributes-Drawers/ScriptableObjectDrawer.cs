using Enderlook.Unity.Utils.UnityEditor;

using System.Reflection;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    [InitializeOnLoad]
    internal static class ScriptableObjectDrawer
    {
        private static GUIContent GUI_CONTENT = new GUIContent("Scriptable Object Menu", "Open the Scriptable Object Menu.");
        static ScriptableObjectDrawer()
        {
            EditorApplication.contextualPropertyMenu += (GenericMenu menu, SerializedProperty property) =>
            {
                FieldInfo fieldInfo = property.GetFieldInfo();
                if (typeof(ScriptableObject).IsAssignableFrom(fieldInfo.FieldType))
                    menu.AddItem(
                        GUI_CONTENT,
                        false,
                        () => ScriptableObjectWindow.CreateWindow(property, fieldInfo)
                    );
            };
        }
    }
}
