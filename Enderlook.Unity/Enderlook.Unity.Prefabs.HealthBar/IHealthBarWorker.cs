using System;

namespace Enderlook.Unity.Prefabs.HealthBarGUI
{
    public interface IHealthBarWorker
    {
        /// <summary>
        /// Modify the health bar values without producing any animation effects (sliding the bar or changing the numbers).<br/>
        /// The health bar fill will be instantaneously set without producing animation. Health numbers will also change immediately.<br/>
        /// Both damage bar and healing bar fill will be set to 0, halting any current animation on them.<br/>
        /// Designed to initialize the health bar by first time.
        /// </summary>
        /// <param name="health"></param>
        /// <param name="maxHealth"></param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="health"/> or <paramref name="maxHealth"/> are negative.</exception>
        void ManualUpdate(float health, float maxHealth);

        /// <summary>
        /// Modify the health bar values without producing any animation effects (sliding the bar or changing the numbers).<br/>
        /// The health bar fill will be instantaneously set without producing animation. Health numbers will also change immediately.<br/>
        /// Both damage bar and healing bar fill will be set to 0, halting any current animation on them.<br/>
        /// Both current health and maximum health will be assigned by maxHealth.<br/>
        /// Designed to initialize the health bar by first time.
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="health"/> is negative.</exception>
        /// </summary>
        void ManualUpdate(float health);

        /// <summary>
        /// Modify the current health and maximum health.<br/>
        /// This method will automatically calculate, show and animate the health bar, damage bar, healing bar and health number.
        /// </summary>
        /// <param name="health">Current health value.</param>
        /// <param name="maxHealth">Maximum health value.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="health"/> or <paramref name="maxHealth"/> are negative.</exception>
        void UpdateValues(float health, float maxHealth);

        /// <summary>
        /// Finish instantaneously the animation of current health bar, damage bar, healing bar and health number. 
        /// </summary>
        void FinishCurrentAnimation();

        /// <summary>
        /// Modify the current health.<br/>
        /// This method will automatically calculate, show and animate the health bar, damage bar, healing bar and health number.
        /// </summary>
        /// <param name="health">Current health value.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="health"/> ise negative.</exception>
        void UpdateValues(float health);

        /// <summary>
        /// Modify current health and update animation if necessary.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <see langword="value"/> is negative.</exception>
        int Health { set; }

        /// <summary>
        /// Modify maximum health and update animation if necessary.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <see langword="value"/> is negative.</exception>
        int MaxHealth { set; }
    }
}