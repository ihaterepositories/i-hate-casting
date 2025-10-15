using System;

namespace Models.WorldObjects.Creatures.Base.Living.Interfaces
{
    public interface IHealthService
    {
        public float CurrentValue { get; }
        
        public void ChangeBy(float value, float maxHealth);
        public void Refresh(float maxHealth);
        
        public event Action OnDamaged;
        public event Action OnHealed;
        public event Action OnHealthGone;
        
        /// <summary>
        /// Transmits current and maximum health values.
        /// </summary>
        public event Action<float, float> OnHealthChanged; 
    }
}