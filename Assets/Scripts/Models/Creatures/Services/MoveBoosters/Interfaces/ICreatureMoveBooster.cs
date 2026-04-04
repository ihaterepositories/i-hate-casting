using System;

namespace Models.Creatures.Services.MoveBoosters.Interfaces
{
    /// <summary>
    /// Handles temporary movement speed boosting for a creature under specific conditions.
    /// </summary>
    public interface ICreatureMoveBooster
    {
        /// <summary>
        /// Total duration of the boost cooldown.
        /// </summary>
        public float BoostCooldownDuration { get; }
        
        /// <summary>
        /// Time elapsed since the boost cooldown started.
        /// </summary>
        public float BoostCooldownTimeElapsed { get; }

        /// <summary>
        /// Invoked every time when a movement boost is activated.
        /// </summary>
        public event Action OnBoostActivated;
        
        /// <summary>
        /// Performs one update step: checks activation conditions.
        /// Should be called every frame (e.g., from Update).
        /// </summary>
        public void Tick();

        /// <summary>
        /// Performs one fixed update step: starts boosting if applicable, and updates internal timers (boost and boost cooldown timers).
        /// Should be called every fixed frame (e.g., from FixedUpdate).
        /// </summary>
        public void FixedTick();
    }
}