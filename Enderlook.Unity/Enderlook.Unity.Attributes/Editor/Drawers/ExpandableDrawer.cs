using Enderlook.Unity.Utils.UnityEditor;

using System;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    //[CustomPropertyDrawer(typeof(UnityEngine.Object), true)] // Will affect all objects
    //[CustomPropertyDrawer(typeof(ScriptableObject), true)] // Will only affect scriptable objects
    [CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
    internal class ExpandableDrawer : SmartPropertyDrawer
    {
        // https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/

        // Cached scriptable object editor
        private Editor editor;

        // Whenever wrap fields in a box
        private const bool USE_BOX = true;

        // How color is multiplied in the area fields
        private const float COLOR_MULTIPLIER = .9f;

        // Whenever it should show a button to open it as a window
        private const bool SHOW_BUTTON = false;

        private static readonly GUIStyle popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
        {
            imagePosition = ImagePosition.ImageOnly
        };

        static ExpandableDrawer()
        {
            if (COLOR_MULTIPLIER > 1 || COLOR_MULTIPLIER <= 0)
                throw new ArgumentOutOfRangeException(nameof(COLOR_MULTIPLIER), COLOR_MULTIPLIER, "Must be greater than 0 and lower or equal 1.");
        }

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
        {
            object reference = property.objectReferenceValue;

            ExpandableAttribute expandableAttribute = (ExpandableAttribute)attribute;

            if (expandableAttribute.showButton ?? SHOW_BUTTON)
            {
                // Show field label
                Rect newPosition = EditorGUI.PrefixLabel(position, label);

                // Calculate rect for button
                Rect buttonRect = new Rect(
                    newPosition.x,
                    newPosition.y + popupStyle.margin.top,
                    popupStyle.fixedWidth + popupStyle.margin.right - (EditorGUI.indentLevel * popupStyle.fixedWidth),
                    newPosition.height - popupStyle.margin.top
                );

                if (GUI.Button(buttonRect, GUIContent.none, popupStyle))
                    ExpandableWindow.CreateWindow(property);

                newPosition.xMin += buttonRect.width;

                EditorGUI.PropertyField(newPosition, property, GUIContent.none, true);
            }
            else
                EditorGUI.PropertyField(position, property, label, true);

            Type type = property.serializedObject.targetObject.GetType();
            if (!type.IsSubclassOf(typeof(UnityEngine.Object)))
            {
                Debug.LogError($"{nameof(ExpandableAttribute)} can only be used on types subclasses of {nameof(UnityEngine.Object)}. {property.name} from {property.GetParentTargetObjectOfProperty()} (path {property.propertyPath}) is type {type}.");
                return;
            }

            // If we have a value
            if (reference != null)
                // We can make the field expandable with a Foldout
                // No GUIContent because the property field below already has it.
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none, true);

            // If the foldout is expanded
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                if (editor == null)
                    Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
                // Check again because it may not be created by the Editor.CreateChachedEditor
                if (editor != null)
                {
                    #region Style
                    // https://forum.unity.com/threads/giving-unitygui-elements-a-background-color.20510/
                    bool isBoxed = expandableAttribute.isBoxed ?? USE_BOX;
                    Color color = GUI.backgroundColor;
                    float colorMultiplier = expandableAttribute.colorMultiplier ?? COLOR_MULTIPLIER;
                    if (isBoxed)
                    {
                        if (colorMultiplier != 1)
                            GUI.backgroundColor = new Color(color.r * colorMultiplier, color.g * colorMultiplier, color.b * colorMultiplier);
                        GUILayout.BeginVertical("box");
                    }
                    else
                        if (colorMultiplier != 1)
                    {
                        GUIStyle guiStyle = new GUIStyle();
                        Texture2D texture2D = new Texture2D(1, 1);
                        texture2D.SetPixel(0, 0, new Color(0, 0, 0, 1 - colorMultiplier));
                        texture2D.Apply();
                        guiStyle.normal.background = texture2D;
                        GUILayout.BeginVertical(guiStyle);
                    }
                    else
                        GUILayout.BeginVertical();
                    #endregion
                    EditorGUI.BeginChangeCheck();
                    editor.OnInspectorGUI();
                    if (EditorGUI.EndChangeCheck())
                        property.serializedObject.ApplyModifiedProperties();
                    GUILayout.EndVertical();
                    #region Style
                    if (colorMultiplier != 1)
                        GUI.backgroundColor = color;
                    #endregion
                }
                EditorGUI.indentLevel--;
            }
        }
    }
}