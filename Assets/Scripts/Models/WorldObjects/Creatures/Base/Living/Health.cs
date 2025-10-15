using System;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;

namespace Models.WorldObjects.Creatures.Base.Living
{
    public class Health : IHealthService
    {
        private float _currentHealth;
        
        public float CurrentValue => _currentHealth;

        public event Action OnDamaged;
        public event Action OnHealed;
        public event Action OnHealthGone;
        public event Action<float, float> OnHealthChanged; 

        public void ChangeBy(float value, float maxHealth)
        {
            if (value < 0f)
                OnDamaged?.Invoke();
            else if (value > 0f)
                OnHealed?.Invoke();
            
            if (value >= maxHealth)
            {
                _currentHealth = maxHealth;
                OnHealthChanged?.Invoke(_currentHealth, maxHealth);
                return;
            }
            
            _currentHealth += value;
            OnHealthChanged?.Invoke(_currentHealth < 0 ? 0 : _currentHealth, maxHealth);
            
            if (_currentHealth <= 0f)
                OnHealthGone?.Invoke();
        }

        public void Refresh(float maxHealth)
        {
            _currentHealth = maxHealth;
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
        }
    }
}