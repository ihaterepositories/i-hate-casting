using System;

namespace Models.Creatures.Services.Health.Interfaces
{
    /// <summary>
    /// Represents creature health, providing access to current and maximum values,
    /// as well as methods for modifying health and notifying about changes.
    /// </summary>
    public interface ICreatureHealth
    {
        /// <summary>
        /// Current health value. If value lower than 0 - returns 0.
        /// </summary>
        public float CurrentValue { get; }
    
        /// <summary>
        /// Maximum possible health value for this creature.
        /// </summary>
        public float MaxValue { get; }
    
        /// <summary>
        /// Invoked when the creature takes damage (health decreases).
        /// </summary>
        public event Action OnDamaged;
    
        /// <summary>
        /// Invoked when the creature is healed (health increases).
        /// </summary>
        public event Action OnHealed;
    
        /// <summary>
        /// Invoked when health reaches zero.
        /// </summary>
        public event Action OnHealthGone;
    
        /// <summary>
        /// Invoked whenever the health value changes.
        /// </summary>
        public event Action OnHealthChanged; 
    
        /// <summary>
        /// Changes health by the specified value.
        /// Positive values heal, negative values deal damage.
        /// </summary>
        /// <param name="value">Amount to change health by.</param>
        void ChangeBy(float value);
    
        /// <summary>
        /// Restores health to its maximum value.
        /// </summary>
        void Refresh();
    }
}