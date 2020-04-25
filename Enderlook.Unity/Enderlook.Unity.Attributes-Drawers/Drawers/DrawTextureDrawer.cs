using Enderlook.Utils.Exceptions;

using UnityEditor;

using UnityEngine;

namespace Enderlook.Unity.Attributes
{
    //[CustomPropertyDrawer(typeof(Sprite))]
    [CustomPropertyDrawer(typeof(DrawTextureAttribute))]
    internal class DrawTextureDrawer : SmartPropertyDrawer
    {
        private float additionalHeight;
        private Texture texture;

        protected override void OnGUISmart(Rect position, SerializedProperty property, GUIContent label)
        {
            SetTexture(property);

            Rect mainPosition = position;
            Rect texturePosition;

            if (attribute is DrawTextureAttribute drawTextureAttribute)
            {
                float height = drawTextureAttribute.height;
                if (height == -1)
                    height = position.height;

                float width = drawTextureAttribute.width;
                if (width == -1)
                    width = height;

                void SetTexturePositionInNewLine()
                {
                    // Not exactly sure why half of height. I think it's because additionalHeight is computed twice due GetPropertyHeight. Not sure...
                    additionalHeight = height / 2;

                    float x = position.x;
                    if (drawTextureAttribute.centered)
                        x += position.width / 2;

                    // We must deduct the additional height that is added by GetPropertyHeight
                    texturePosition = new Rect(x, position.y + position.height - additionalHeight, width, height);
                }

                void SetTexturePositionInSameLine()
                {
                    texturePosition = new Rect(position.x + mainPosition.width, position.y, height, width);
                    additionalHeight = 0;
                }

                switch (drawTextureAttribute.hideMode)
                {
                    case DrawTextureAttribute.Hide.All:
                        additionalHeight = 0;
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
            if (Event.current.type != EventType.Repaint && property.objectReferenceValue != null)
                return;

            if (property.objectReferenceValue is Sprite sprite)
            {
                GUIContent content = new GUIContent
                {
                    tooltip = label.text + "\n" + label.tooltip,
                    image = sprite.texture
                };
                EditorGUI.LabelField(texturePosition, content);
            }
            else if (property.objectReferenceValue != null)
            {
                EditorGUI.HelpBox(texturePosition, $"Doesn't have an object of type {typeof(Sprite)}.", MessageType.Error);
                Debug.LogError($"Property {property.displayName} from {property.propertyPath} does't have an object of type {typeof(Sprite)}.");
            }

            void DrawDefault(float h, float w)
            {
                mainPosition = new Rect(position.x, position.y, position.width - h, position.height);
                texturePosition = new Rect(position.x + mainPosition.width, position.y, h, w);
                EditorGUI.PropertyField(mainPosition, property, label, true);
            }
        }

        private void SetTexture(SerializedProperty property)
        {
            if (property.objectReferenceValue is Texture2D texture2D)
                texture = texture2D;
            else if (property.objectReferenceValue is Sprite sprite)
                texture = sprite.texture;
            else
                texture = null;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => base.GetPropertyHeight(property, label) + (texture == null ? 0 : additionalHeight);
    }
}