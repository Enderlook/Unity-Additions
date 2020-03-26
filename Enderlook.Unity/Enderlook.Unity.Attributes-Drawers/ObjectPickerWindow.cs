using Enderlook.Unity.Utils.UnityEditor;

using System;
using System.Linq;
using System.Reflection;

using UnityEditor;

using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace Enderlook.Unity.Attributes
{
    public class ObjectPickerWindow : EditorWindow
    {
        private static readonly GUIContent CONTEXT_PROPERTY_MENU = new GUIContent("Object Picker Menu", "Open the Object Picker Menu");
        private static readonly GUIContent TILE_CONTENT = new GUIContent("Object Picker Menu");

        private PropertyWrapper propertyWrapper;

        private RestrictTypeAttribute restrictTypeAttribute;

        private UnityObject[] objects;

        private string[] objectsLabel;

        private bool gatherFromAssets = false;

        private int index = -1;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        [InitializeOnLoadMethod]
        private static void AddContextualPropertyMenu()
        {
            ContextualPropertyMenu.contextualPropertyMenu += (GenericMenu menu, SerializedProperty property) =>
            {
                if (property.propertyPath.EndsWith(".Array.Size"))
                    return;

                FieldInfo fieldInfo = property.GetFieldInfo();
                if (typeof(UnityObject).IsAssignableFrom(fieldInfo.FieldType))
                    menu.AddItem(
                        CONTEXT_PROPERTY_MENU,
                        false,
                        () => CreateWindow(property, fieldInfo)
                    );
            };
        }

        private static void CreateWindow(SerializedProperty property, FieldInfo fieldInfo)
        {
            ObjectPickerWindow window = GetWindow<ObjectPickerWindow>();

            window.propertyWrapper = new PropertyWrapper(property, fieldInfo);

            window.restrictTypeAttribute = fieldInfo.GetCustomAttribute<RestrictTypeAttribute>();

            window.RefeshObjects();
        }

        private void RefeshObjects()
        {
            UnityObject selected = null;
            if (index != -1)
                selected = objects[index];

            if (gatherFromAssets)
                objects = Resources.FindObjectsOfTypeAll(propertyWrapper.Type);
            else
                objects = FindObjectsOfType(propertyWrapper.Type);

            if (restrictTypeAttribute != null)
                objects = objects.Where(e => restrictTypeAttribute.CheckIfTypeIsAllowed(e.GetType())).ToArray();

            objectsLabel = objects.Select(e => e.ToString()).ToArray();

            if (index != -1)
            {
                index = Array.IndexOf(objects, selected);
                if (index == -1)
                    index = Array.IndexOf(objects, propertyWrapper.Get());
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnGUI()
        {
            titleContent = TILE_CONTENT;

            bool oldGatherFromAssets = gatherFromAssets;
            gatherFromAssets = EditorGUILayout.Toggle(new GUIContent("Include Assets", "Whenever it should also look for in asset files"), gatherFromAssets);
            if (oldGatherFromAssets != gatherFromAssets)
                RefeshObjects();

            if (GUILayout.Button(new GUIContent("Refesh Search", "Search again for objects")))
                RefeshObjects();

            index = EditorGUILayout.Popup(index, objectsLabel);

            EditorGUI.BeginDisabledGroup(index == -1);
            if (GUILayout.Button(new GUIContent("Apply", "Assign object to field")))
            {
                propertyWrapper.Set(objects[index]);
                propertyWrapper.ApplyModifiedProperties();
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}