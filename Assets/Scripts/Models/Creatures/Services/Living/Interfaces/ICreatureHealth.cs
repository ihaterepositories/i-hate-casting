using System;

namespace Models.Creatures.Services.Living.Interfaces
{
    public interface ICreatureHealth
    {
        /// <summary>
        /// Returns normalized* current health value.
        /// *If health is lower than 0, returns 0.
        /// </summary>
        public float CurrentValue { get; }
        
        /// <summary>
        /// Returns maximum possible health value for this creature.
        /// </summary>
        public float MaxValue { get; }
        
        public void ChangeBy(float value);
        public void Refresh();
        
        public event Action OnDamaged;
        public event Action OnHealed;
        public event Action OnHealthGone;
        public event Action OnHealthChanged; 
    }
}