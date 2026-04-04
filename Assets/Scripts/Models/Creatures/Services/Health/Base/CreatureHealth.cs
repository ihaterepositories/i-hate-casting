using System;
using Models.Creatures.Dtos;
using Models.Creatures.Services.StatsScalers.Interfaces;

namespace Models.Creatures.Services.Health.Base
{
    public abstract class CreatureHealth
    {
        private readonly CreatureStats _stats;
        private readonly ICreatureStatsScaler _statsScaler;
        
        protected float _currentHealth;

        protected CreatureHealth(
            CreatureStats stats,
            ICreatureStatsScaler statsScaler)
        {
            _stats = stats;
            _statsScaler = statsScaler;
            
            _currentHealth = MaxValue;
        }
        
        public float CurrentValue => _currentHealth < 0 ? 0 : _currentHealth;
        public float MaxValue => _statsScaler.ScaleMaxHealth(_stats.MaxHealth);

        public event Action OnDamaged;
        public event Action OnHealed;
        public event Action OnHealthGone;
        public event Action OnHealthChanged;

        public abstract void ChangeBy(float value);

        public void Refresh()
        {
            _currentHealth = MaxValue;
            OnHealthChanged?.Invoke();
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
            OnHealthChanged?.Invoke();
        }
    }
}