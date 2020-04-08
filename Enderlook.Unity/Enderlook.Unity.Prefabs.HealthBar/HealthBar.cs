using Enderlook.Unity.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Enderlook.Unity.Prefabs.HealthBarGUI
{
    [AddComponentMenu("Enderlook/Health Bar")]
    public class HealthBar : MonoBehaviour, IHealthBar
    {
#pragma warning disable CS0649
        [Header("Configuration")]
        [SerializeField, Tooltip("How numbers are shown, {0} is health, {1} is maximum health and {2} is percent of health. Eg: {0} / {1} ({2}%)")]
        private string textShowed = "{0} / {1} ({2}%)";

        [SerializeField, Tooltip("If damage or healing bars are active you can choose to add dynamic numbers.")]
        private bool dynamicNumbers;

        [SerializeField, Tooltip("Health bar color (usually at max health). Use black color to use the Health image UI color.")]
        private Color maxHealthColor = Color.green;

        [SerializeField, Tooltip("Health bar color at minimum health. If black, health won't change of color at low health.")]
        private Color minHealthColor = Color.red;

        [SerializeField, Tooltip("Filling bars speed."), Range(0.1f, 10)]
        private float fillingSpeed = 1;

        [Header("Setup")]
        [SerializeField, Tooltip("Used to show numbers of health. Use null to deactivate it.")]
        private Text textNumber;

        [SerializeField, Tooltip("Represent object health.")]
        private GameObject healthBar;

        private Image healthImage;
        private RectTransform healthTransform;

        [SerializeField, Tooltip("Represent the amount of recent damage received. Use null to deactivate it.")]
        private Image damageBar;

        [SerializeField, Tooltip("Represent the amount of recent healing received. Use null to deactivate it.")]
        private GameObject healingBar;

        private Image healingImage;
        private RectTransform healingTransform;

        [SerializeField, Tooltip("How decimal values are rounded.")]
        private RoundingMode roundingMode;

        [Header("Hidding Setup")]
        [SerializeField, Tooltip("Used to show or hide the health bar. If null, it will show and hide each part by separate instead of just the canvas.")]
        private Canvas canvas;

        [SerializeField, Tooltip("Only used to hide or show in case Canvas is null.")]
        private Image frame;

        [SerializeField, Tooltip("Only used to hide or show in case Canvas is null.")]
        private Image background;
#pragma warning restore CS0649

        private float maxHealth;
        private float health;

        /// <inheritdoc cref="IHealthBarViewer.IsVisible"/>
        public bool IsVisible {
            get => canvas != null ? canvas.enabled : isVisible;
            set {
                isVisible = value;
                if (canvas != null)
                    canvas.enabled = isVisible;
                else
                {
                    healthImage.enabled = isVisible;
                    if (textNumber != null)
                        textNumber.enabled = isVisible;
                    if (damageBar != null)
                        damageBar.enabled = isVisible;
                    if (healingImage != null)
                        healingImage.enabled = isVisible;
                    if (frame != null)
                        frame.enabled = isVisible;
                    if (background != null)
                        background.enabled = isVisible;
                }
            }
        }

        private bool isVisible;
        /// <inheritdoc cref="IHealthBarViewer.IsEnabled"/>
        public bool IsEnabled { get; set; } = true;

        private void Awake() => Setup();

        /// <inheritdoc cref="IHealthBarWorker.ManualUpdate(float, float)"/>
        public void ManualUpdate(float health, float maxHealth)
        {
            this.health = health;
            this.maxHealth = maxHealth;

            FinishCurrentAnimation();
        }

        /// <inheritdoc cref="IHealthBarWorker.ManualUpdate(float)"/>
        public void ManualUpdate(float maxHealth) => ManualUpdate(maxHealth, maxHealth);

        /// <inheritdoc cref="IHealthBarWorker.FinishCurrentAnimation"/>
        public void FinishCurrentAnimation()
        {
            // Fix bug, this shouldn't be happening
            if (healthImage == null)
                healthImage = healthBar.GetComponent<Image>();

            healthImage.fillAmount = health / maxHealth;
            if (damageBar != null)
                damageBar.fillAmount = 0;
            if (healingImage != null)
                healingImage.fillAmount = 0;

            UpdateNonDynamicNumber();
        }

        /// <summary>
        /// Get the <see cref="healthImage"/> color taking into account the percentage of remaining health.
        /// </summary>
        /// <returns>Color of the <see cref="healthImage"/></returns>
        private Color GetHealthColor() => Color.Lerp(minHealthColor, maxHealthColor, healthImage.fillAmount + (damageBar != null ? damageBar.fillAmount : 0) - (healingBar != null ? healingImage.fillAmount : 0));

        private void Update()
        {
            if (IsEnabled)
            {
                // Unfill the damage and healing bar per frame
                if (damageBar != null && damageBar.fillAmount > 0)
                    damageBar.fillAmount -= Time.deltaTime * fillingSpeed;
                if (healingImage != null && healingImage.fillAmount > 0)
                    healingImage.fillAmount -= Time.deltaTime * fillingSpeed;

                healthImage.color = minHealthColor != Color.black ? GetHealthColor() : maxHealthColor;

                if (textNumber != null)
                    if (dynamicNumbers)
                    {
                        float dynamicPercent = healthImage.fillAmount + damageBar.fillAmount - healingImage.fillAmount,
                              dynamicHealth = maxHealth * dynamicPercent;
                        textNumber.text = string.Format(textShowed, roundingMode.Round(dynamicHealth), roundingMode.Round(maxHealth), roundingMode.Round(dynamicHealth / maxHealth * 100));
                    }
                    else
                        UpdateNonDynamicNumber();
            }
        }

        private void UpdateNonDynamicNumber() => textNumber.text = string.Format(textShowed, roundingMode.Round(health), roundingMode.Round(maxHealth), roundingMode.Round(health / maxHealth * 100));

        /// <inheritdoc cref="IHealthBarWorker.UpdateValues(float, float)"/>
        public void UpdateValues(float health, float maxHealth)
        {
            this.maxHealth = maxHealth;
            Set(health);
        }

        /// <inheritdoc cref="IHealthBarWorker.UpdateValues(float)"/>
        public void UpdateValues(float health) => Set(health);

        /// <summary>
        /// Set the new health value and updates bars.
        /// </summary>
        /// <seealso cref="Change(float)"/>
        /// <param name="value">New <seealso cref="health"/> value.</param>
        private void Set(float value) => Change(value - health);

        /// <summary>
        /// Updates bars and set the <see cref="health"/>.
        /// </summary>
        /// <param name="amount">Amount to add to <see cref="health"/>.</param>
        private void Change(float amount)
        {
            if (amount == 0)
                return;

            float old_health = health;
            health += amount;

            // Don't allow health be greater than maximum health nor lower than 0
            if (health > maxHealth)
            {
                health = maxHealth;
                amount = maxHealth - old_health;
            }
            else if (health < 0)
            {
                health = 0;
                amount = old_health;
            }

            // Fill the health bar
            healthImage.fillAmount = health / maxHealth;

            if (amount < 0)
            {
                amount = -amount;
                if (damageBar != null)
                {
                    damageBar.fillAmount += amount / maxHealth;
                    // Move the damage bar adjacent (next to the end) of the health bar
                    damageBar.transform.localPosition = new Vector3(healthTransform.rect.width * healthImage.fillAmount, 0, 0);
                }
                if (healingBar != null)
                {
                    // On damage, the healing bar is reduced to avoid overlapping
                    healingImage.fillAmount -= amount / maxHealth;
                    healingBar.transform.localPosition = new Vector3(healthTransform.rect.width * healthImage.fillAmount - healingTransform.rect.width, 0, 0);
                }
            }
            else if (amount > 0)
            {
                if (healingBar != null)
                {
                    healingImage.fillAmount += amount / maxHealth;
                    // Move the healing bar adjacent (next to the end) of the health bar but overlap part of it by its filled part
                    healingBar.transform.localPosition = new Vector3(healthTransform.rect.width * healthImage.fillAmount - healingTransform.rect.width, 0, 0);
                }
                if (damageBar != null)
                {
                    // On healing, the damage bar is reduced to avoid overlapping
                    damageBar.fillAmount -= amount / maxHealth;
                    damageBar.transform.localPosition = new Vector3(healthTransform.rect.width * healthImage.fillAmount, 0, 0);
                }
            }
            // Fix bug, preventing the healing bar overflow from the left side
            if (healingBar != null && healingTransform.rect.width < healingTransform.rect.width * healingImage.fillAmount - healingBar.transform.localPosition.x)
                healingImage.fillAmount -= (healingTransform.rect.width * healingImage.fillAmount - healingBar.transform.localPosition.x - healingTransform.rect.width) / healingTransform.rect.width;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Update color of health bar.
        /// </summary>
        private void OnValidate()
        {
            Setup();
            if ((healthImage = healthBar.GetComponent<Image>()) == null)
                Debug.LogWarning($"Gameobject {gameObject.name} has a {nameof(healthBar)} which lacks of {nameof(Image)} component.");
            else
                healthImage.color = GetHealthColor();
        }
#endif

        /// <summary>
        /// Get the require components <seealso cref="healthImage"/>, <seealso cref="healthTransform"/>, <seealso cref="healingImage"/>, <seealso cref="healingTransform"/> and set <seealso cref="maxHealth"/>.
        /// </summary>
        private void Setup()
        {
            healthImage = healthBar.GetComponent<Image>();
            healthTransform = healthBar.GetComponent<RectTransform>();

            if (healingBar != null)
            {
                healingImage = healingBar.GetComponent<Image>();
                healingTransform = healingBar.GetComponent<RectTransform>();
            }

            if (maxHealthColor == Color.black)
                maxHealthColor = healthImage.color;
        }

        /// <inheritdoc cref="IHealthBarViewer.HealingBarPercentFill"/>
        public float HealthBarPercentFill => healthImage.fillAmount;

        /// <inheritdoc cref="IHealthBarViewer.HealingBarPercentFill"/>
        public float? HealingBarPercentFill => healingImage != null ? healingImage.fillAmount : (float?)null;

        /// <inheritdoc cref="IHealthBarViewer.DamageBarPercentFill"/>
        public float? DamageBarPercentFill => damageBar != null ? damageBar.fillAmount : (float?)null;

        /// <inheritdoc cref="IHealthBarViewer.IsHealingBarPercentHide"/>
        public bool IsHealingBarPercentHide => healingImage == null ? true : healingImage.fillAmount == 0;

        /// <inheritdoc cref="IHealthBarViewer.IsDamageBarPercentHide"/>
        public bool IsDamageBarPercentHide => damageBar == null ? true : damageBar.fillAmount == 0;

        /// <inheritdoc cref="IHealthBarWorker.Health"/>
        public int Health {
            set {
                if (health != value)
                    Set(value);
            }
        }

        /// <inheritdoc cref="IHealthBarWorker.MaxHealth"/>
        public int MaxHealth {
            set => maxHealth = value;
        }
    }
}