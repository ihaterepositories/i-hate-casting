using System;
using Models.Creatures.Services.StatsCalculating.Interfaces;

namespace Models.Creatures.Services.Living.Base
{
    public abstract class CreatureHealth
    {
        private readonly ICreatureStatsCalculator _statsCalculateService;
        
        protected float _currentHealth;

        protected CreatureHealth(ICreatureStatsCalculator statsCalculateService)
        {
            _statsCalculateService = statsCalculateService;
            
            _currentHealth = MaxValue;
        }
        
        public float CurrentValue => _currentHealth < 0 ? 0 : _currentHealth;
        public float MaxValue => _statsCalculateService.CalculateMaxHealth();

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