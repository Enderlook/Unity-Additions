using Enderlook.Exceptions;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    //[CustomPropertyDrawer(typeof(Sprite))]
    [CustomPropertyDrawer(typeof(DrawTextureAttribute))]
    internal class DrawTextureDrawer : SmartPropertyDrawer
    {
        private const int INDENT_WIDTH = 8; // TODO: This is wrong.

        private GUIContent textureContent;

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect mainPosition = position;
            Rect texturePosition;

            if (attribute is DrawTextureAttribute drawTextureAttribute)
            {
                float height = CalculateTextureHeight(position.height, drawTextureAttribute);
                if (textureContent != null)
                    mainPosition.height -= height + EditorGUIUtility.singleLineHeight;

                float width = drawTextureAttribute.width;
                if (width == -1)
                    width = height;

                void SetTexturePositionInNewLine()
                {
                    float x = position.x;
                    if (drawTextureAttribute.centered)
                        x += position.width / 2;

                    float width_ = width - INDENT_WIDTH;
                    float height_ = width_;
                    if (textureContent != null && textureContent.image != null)
                        height_ = height_ / textureContent.image.width * textureContent.image.height;
                    texturePosition = new Rect(x + INDENT_WIDTH, position.y + EditorGUIUtility.singleLineHeight, width_, Mathf.Min(height, height_));
                }

                void SetTexturePositionInSameLine()
                {
                    float width_ = height;
                    if (textureContent != null && textureContent.image != null)
                        width_ = width_ / textureContent.image.height * textureContent.image.width;
                    texturePosition = new Rect(position.x + mainPosition.width, position.y, Mathf.Min(width, width_), height);
                }

                switch (drawTextureAttribute.hideMode)
                {
                    case DrawTextureAttribute.Hide.All:
                        texturePosition = new Rect(position.x, position.y, height, width);
                        break;
                    case DrawTextureAttribute.Hide.None:
                        if (drawTextureAttribute.drawOnSameLine)
                            DrawDefault(height, width);
                        else
                        {
                            SetTexturePositionInNewLine();
                            EditorGUI.PropertyField(mainPosition, property, label, true);
                        }
                        break;
                    case DrawTextureAttribute.Hide.Label:
                        if (drawTextureAttribute.drawOnSameLine)
                        {
                            mainPosition = new Rect(position.x, position.y, position.width - Mathf.Max(GUI.skin.label.CalcSize(label).x, height), position.height);
                            SetTexturePositionInSameLine();
                        }
                        else
                            SetTexturePositionInNewLine();
                        EditorGUI.PropertyField(mainPosition, property, GUIContent.none, true);
                        break;
                    case DrawTextureAttribute.Hide.Field:
                        if (drawTextureAttribute.drawOnSameLine)
                        {
                            mainPosition = new Rect(position.x, position.y, Mathf.Min(GUI.skin.label.CalcSize(label).x, height), position.height);
                            SetTexturePositionInSameLine();
                        }
                        else
                            SetTexturePositionInNewLine();
                        EditorGUI.LabelField(mainPosition, label);
                        break;
                    default:
                        throw new ImpossibleStateException();
                }
            }
            else
                DrawDefault(position.height, position.width);

            // If we have an sprite to show and we are in a repaint event to show it
            if (Event.current.type != EventType.Repaint && HasContent(property))
                return;

            ProduceTextureGUIContent(property, label);

            if (textureContent.image != null)
                EditorGUI.LabelField(texturePosition, textureContent);
            else
                CheckPropertyType(property, texturePosition);

            void DrawDefault(float h, float w)
            {
                mainPosition = new Rect(position.x, position.y, position.width - h, position.height);
                texturePosition = new Rect(position.x + mainPosition.width, position.y, h, w);
                EditorGUI.PropertyField(mainPosition, property, label, true);
            }
        }

        private void ProduceTextureGUIContent(SerializedProperty property, GUIContent label)
        {
            Texture texture = GetTexture(property);
            if (textureContent is null)
                textureContent = new GUIContent
                {
                    tooltip = label.text + "\n" + label.tooltip,
                    image = texture
                };
            else if (textureContent.image != texture)
                textureContent.image = texture;
        }

        private static bool HasContent(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue != null;
                case SerializedPropertyType.String:
                    return !string.IsNullOrEmpty(property.stringValue);
            }
            return false;
        }

        private static Texture GetTexture(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                    if (property.objectReferenceValue is Texture2D texture2D)
                        return texture2D;
                    else if (property.objectReferenceValue is Sprite sprite)
                        return sprite.texture;
                    break;
                case SerializedPropertyType.String:
                    string path = property.stringValue;
                    Texture texture = Resources.Load<Texture2D>(path);
                    if (path == null)
                    {
                        Sprite sprite = Resources.Load<Sprite>(path);
                        if (path != null)
                            texture = sprite.texture;
                    }
                    return texture;
            }
            return null;
        }

        private void CheckPropertyType(SerializedProperty property, Rect texturePosition)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                    if (fieldInfo.FieldType != typeof(Sprite) || fieldInfo.FieldType != typeof(Texture2D))
                        goto default;
                    break;
                case SerializedPropertyType.String:
                    break;
                default:
                    EditorGUI.HelpBox(texturePosition, $"Doesn't have an object of type {typeof(Sprite)}, {typeof(Texture2D)} nor {typeof(string)}..", MessageType.Error);
                    Debug.LogError($"Property {property.displayName} from {property.propertyPath} does't have an object of type {typeof(Sprite)}, {typeof(Texture2D)} nor {typeof(string)}.");
                    break;
            }
        }

        private static float CalculateTextureHeight(float height, DrawTextureAttribute drawTextureAttribute)
        {
            float height_ = drawTextureAttribute.height;
            if (height_ == -1)
                return height;
            return height;
        }

        protected override float GetPropertyHeightSmart(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUI.GetPropertyHeight(property, label);
            return CalculateAditionalPropertyHeight(property, height);
        }

        private float CalculateAditionalPropertyHeight(SerializedProperty property, float height)
        {
            if (attribute is DrawTextureAttribute drawTextureAttribute)
            {
                switch (drawTextureAttribute.hideMode)
                {
                    case DrawTextureAttribute.Hide.All:
                        return height;
                    case DrawTextureAttribute.Hide.None:
                    case DrawTextureAttribute.Hide.Label:
                    case DrawTextureAttribute.Hide.Field:
                        if (drawTextureAttribute.drawOnSameLine)
                            return height;
                        else if (GetTexture(property) != null)
                            return height + EditorGUIUtility.singleLineHeight + CalculateTextureHeight(height, drawTextureAttribute);
                        else
                            return height;
                    default:
                        throw new ImpossibleStateException();
                }
            }
            else
                return height;
        }
    }
}