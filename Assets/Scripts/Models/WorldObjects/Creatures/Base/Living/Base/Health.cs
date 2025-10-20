using System;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;

namespace Models.WorldObjects.Creatures.Base.Living
{
    public abstract class Health : IHealthService
    {
        private readonly CreatureStatsCalculator _statsCalculator;
        
        protected float _currentHealth;

        protected Health(CreatureStatsCalculator statsCalculator)
        {
            _statsCalculator = statsCalculator;
        }
        
        public float CurrentValue => _currentHealth;
        public float MaxValue => _statsCalculator.GetMaxHealth();

        public event Action OnDamaged;
        public event Action OnHealed;
        public event Action OnHealthGone;
        public event Action<float, float> OnHealthChanged;

        public abstract void ChangeBy(float value);

        public void Refresh()
        {
            _currentHealth = MaxValue;
            OnHealthChanged?.Invoke(_currentHealth, MaxValue);
        }

        protected void RaiseDamaged()
        {
            OnDamaged?.Invoke();
        }
        
        protected void RaiseHealed()
        {
            OnHealed?.Invoke();
        }
        
        protected void RaiseHealthGone()
        {
            OnHealthGone?.Invoke();
        }
        
        protected void RaiseHealthChanged()
        {
            OnHealthChanged?.Invoke(_currentHealth < 0 ? 0 : _currentHealth, MaxValue);
        }
    }
}