using Enderlook.Unity.Utils.UnityEditor;

using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    public abstract class AdditionalPropertyDrawer : PropertyDrawer
    {
        public delegate bool ContextMenuItemGenerator(PropertyDrawerInfo propertyDrawerInfo, out bool valid, out GUIContent guiContent, out GenericMenu.MenuFunction menuFunction);

        private static List<ContextMenuItemGenerator> contextMenuItemGenerators = new List<ContextMenuItemGenerator>();

        protected SerializedPropertyHelper helper;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (helper == null)
                helper = property.GetHelper();

            OnGUIAdditional(position, property, label);

            PropertyDrawerInfo propertyDrawerInfo = new PropertyDrawerInfo(fieldInfo, property, label, position);

            Event @event = Event.current;
            if (@event.type == EventType.MouseDown && @event.button == 1 && position.Contains(@event.mousePosition))
            {
                GenericMenu contextMenu = new GenericMenu();
                bool mustShow = false;
                foreach (ContextMenuItemGenerator contextMenuItemGenerator in contextMenuItemGenerators)
                    if (contextMenuItemGenerator(propertyDrawerInfo, out bool valid, out GUIContent content, out GenericMenu.MenuFunction menuFunction))
                    {
                        if (valid)
                            contextMenu.AddItem(content, false, menuFunction);
                        else
                            contextMenu.AddDisabledItem(content, false);

                        mustShow = true;
                    }

                if (mustShow)
                    contextMenu.ShowAsContext();
            }
        }

        protected abstract void OnGUIAdditional(Rect position, SerializedProperty property, GUIContent label);

        protected void OnGUIBase(Rect position, SerializedProperty property, GUIContent label) => base.OnGUI(position, property, label);

        public static void AddContextMenuItemGenerator(ContextMenuItemGenerator contextMenuItemGenerator)
            => contextMenuItemGenerators.Add(contextMenuItemGenerator);

        public static void RemoveContextMenuItemGenerator(ContextMenuItemGenerator contextMenuItemGenerator)
            => contextMenuItemGenerators.Remove(contextMenuItemGenerator);
    }
}