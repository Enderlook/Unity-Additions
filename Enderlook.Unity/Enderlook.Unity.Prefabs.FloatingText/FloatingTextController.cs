using Enderlook.Unity.Attributes;
using Enderlook.Unity.Utils;

using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Prefabs.FloatingText
{
    [AddComponentMenu("Enderlook/Floating Text/Floating Text Controller")]
    public class FloatingTextController : MonoBehaviour
    {
        /// <summary>
        /// On <see langword="false"/>, this instance ignores any method call to spawn a floaing text.
        /// </summary>
        [Header("Configuration")]
        [SerializeField, Tooltip("On false, it ignores method calls to spawn floating text.")]
        public bool isEnable;

        [SerializeField, Tooltip("Spawning point of prefab. If there are several, a random point will be used."), DrawVectorRelativeToTransform]
        private Vector3[] spawningPoints = new Vector3[1];

        [SerializeField, Tooltip("Maximum amount of floating texts at time. New texts will remove old ones. Use 0 for unlimited.")]
        private int maximumAmountFloatingText = 10;

#pragma warning disable CS0649
        [Header("Floating Text Override Configuration")]
        [SerializeField]
        private bool overrideTimeBeforeDestroy;

        [ShowIf(nameof(overrideTimeBeforeDestroy))]
        [SerializeField, Tooltip("Time before self destroy in seconds. If 0, duration of the animation will be used.")]
        private float timeBeforeDestroy;

        [SerializeField]
        private bool overrideTextColor;

        [ShowIf(nameof(overrideTextColor))]
        [SerializeField, Tooltip("Color used by text")]
        private Color textColor = Color.red;

        [SerializeField]
        private bool overrideRandomOffset;

        [ShowIf(nameof(overrideRandomOffset))]
        [SerializeField, Tooltip("Random spawn offset.")]
        private Vector2 randomOffset = Vector2.one;

        [SerializeField]
        private bool overrideScaleMultiplier;

        [ShowIf(nameof(overrideScaleMultiplier))]
        [SerializeField, Tooltip("Multiply the scale of the canvas by this value.")]
        private float scaleMultiplier = 1;

        [SerializeField]
        private bool overrideDigitPrecision;

        [ShowIf(nameof(overrideDigitPrecision))]
        [SerializeField, Tooltip("Digit precision (decimals) for numbers. Whenever a float is given to show, the number is rounded by a certain amount of digits.")]
        private int digitPrecision = 0;

        [SerializeField]
        private bool overrideTypeOfRounding;

        [ShowIf(nameof(overrideTypeOfRounding))]
        [SerializeField, Tooltip("Determines how decimal digits are rounded.")]
        private RoundingMode roundingType = RoundingMode.Round;

        [Header("Setup")]
        [SerializeField, Tooltip("Floating Text prefab.")]
        private GameObject floatingTextPrefab;

        [SerializeField, Tooltip("Parent transform of all floating texts. Just for organization of scene.\nOptional.\nDO NOT USE A MOVING TRANSFORM!")]
        private Transform floatingTextParent;
#pragma warning restore CS0649

        private static Transform floatingTextParentStatic;

        /// <summary>
        /// Set the parent of all Floating Text <see cref="GameObject"/>s spawned by <see cref="FloatingTextController"/>s which <see cref="floatingTextParent"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="floatingTextParent">Parent of all <see cref="FloatingTextItem"/> <see cref="GameObject"/>s.</param>
        public static void SetFloatingTextParentStatic(Transform floatingTextParent) => floatingTextParentStatic = floatingTextParent;

        /// <summary>
        /// Transform used as parent for spawned floating texts.<br/>
        /// <see cref="FloatingTextParent"/> will be returned unless it's <see langword="null"/>. If <see langword="null"/>, <see cref="floatingTextParentStatic"/> will be returned.
        /// </summary>
        private Transform FloatingTextParent {
            get {
                if (floatingTextParent != null)
                    return floatingTextParent;
                else
                    return floatingTextParentStatic;
            }
        }

        /// <summary>
        /// List of all Floating Text game objects spawned by this <see cref="FloatingTextController"/>.<br/>
        /// The list will only store a number of items equal to <see cref="maximumAmountFloatingText"/>. More items will override old ones.
        /// </summary>
        private readonly List<GameObject> floatingTextList = new List<GameObject>();

        /// <summary>
        /// Spawns a floating text and return its <see cref="FloatingTextItem"/> script.<br/>
        /// </summary>
        /// <returns>Instanced <see cref="FloatingTextItem"/> script.</returns>
        private FloatingTextItem SpawnFloatingTextBase()
        {
            GameObject floatingText = floatingTextParent != null ? Instantiate(floatingTextPrefab, FloatingTextParent) : Instantiate(floatingTextPrefab);
            floatingText.transform.position = spawningPoints[Mathf.RoundToInt(Random.Range(0, spawningPoints.Length))] + transform.position;

            AddToFloatingTextList(floatingText);
            return floatingText.GetComponent<FloatingTextItem>();
        }

        /// <summary>
        /// Add the <paramref name="floatingText"/> to <see cref="floatingTextList"/>.<br/>
        /// In addition, it checks if the amount of current floating texts is between the allowed by <see cref="maximumAmountFloatingText"/>. If surpassed, it will destroy them.
        /// </summary>
        /// <param name="floatingText"><see cref="GameObject"/> of a Floating Text</param>
        private void AddToFloatingTextList(GameObject floatingText)
        {
            if (maximumAmountFloatingText > 0 && floatingTextList.Count >= maximumAmountFloatingText)
            {
                Destroy(floatingTextList[0]);
                floatingTextList.RemoveAt(0);
            }
            floatingTextList.Add(floatingText);
        }

        /// <summary>
        /// Spawns a floating text.<br/>
        /// All the configuration don't provided in this method will be replaced by the configuration already set on <see cref="FloatingTextController"/>, or, if also null, on the <see cref="floatingTextPrefab"/>.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <param name="textColor">Color of the text.</param>
        /// <param name="scaleMultiplier">Scale multiplier to current scale.</param>
        /// <param name="timeBeforeDestroy">Time in seconds before destroy itself.</param>
        /// <param name="randomOffset">Random offset applied on spawn of the floating text.</param>
        public void SpawnFloatingText(string text, Color? textColor = null, float? scaleMultiplier = null, float? timeBeforeDestroy = null, Vector2? randomOffset = null)
        {
            if (!isEnable)
                return;

            FloatingTextItem floatingTextScript = SpawnFloatingTextBase();

            floatingTextScript.SetConfiguration(text,
                textColor != null ? textColor : overrideTextColor ? (Color?)this.textColor : null,
                scaleMultiplier != null ? scaleMultiplier : overrideScaleMultiplier ? (float?)this.scaleMultiplier : null,
                timeBeforeDestroy != null ? timeBeforeDestroy : overrideTimeBeforeDestroy ? (float?)this.timeBeforeDestroy : null,
                randomOffset != null ? randomOffset : overrideRandomOffset ? (Vector2?)this.randomOffset : null
            );
        }

        /// <summary>
        /// Spawns a floating text.<br/>
        /// All the configuration don't provided in this method will be replaced by the configuration already set on <see cref="FloatingTextController"/>, or, if also null, on the <see cref="floatingTextPrefab"/>.
        /// </summary>
        /// <param name="number">Number to display.</param>
        /// <param name="numberColor">Color of the number.</param>
        /// <param name="digitPrecision">Amount of decimals able to show (more decimals will be rounded by <paramref name="roundingMode"/>).</param>
        /// <param name="scaleMultiplier">Scale multiplier to current scale.</param>
        /// <param name="timeBeforeDestroy">Time in seconds before destroy itself.</param>
        /// <param name="randomOffset">Random offset applied on spawn of the floating text.</param>
        /// <param name="roundingMode">Type of rounding used to round the number to the given <paramref name="digitPrecision"/></param>
        public void SpawnFloatingText(float number, Color? numberColor = null, int? digitPrecision = null, float? scaleMultiplier = null, float? timeBeforeDestroy = null, Vector2? randomOffset = null, RoundingMode? roundingMode = null)
        {
            if (!isEnable)
                return;

            FloatingTextItem floatingTextScript = SpawnFloatingTextBase();

            floatingTextScript.SetConfiguration(number,
                numberColor != null ? numberColor : textColor,
                scaleMultiplier != null ? scaleMultiplier : this.scaleMultiplier,
                timeBeforeDestroy != null ? timeBeforeDestroy : this.timeBeforeDestroy,
                randomOffset != null ? randomOffset : this.randomOffset,
                digitPrecision != null ? digitPrecision : this.digitPrecision,
                roundingMode != null ? roundingMode : roundingType
            );
        }
    }
}