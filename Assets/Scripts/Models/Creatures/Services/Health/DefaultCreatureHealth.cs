using Models.Creatures.Dtos;
using Models.Creatures.Services.Health.Base;
using Models.Creatures.Services.Health.Interfaces;
using Models.Creatures.Services.StatsScalers.Interfaces;

namespace Models.Creatures.Services.Health
{
    public class DefaultCreatureHealth : CreatureHealth, ICreatureHealth
    {
        public DefaultCreatureHealth(
            CreatureStats stats,
            ICreatureStatsScaler statsScaler) 
            : base(stats, statsScaler)
        {
        }

        public override void ChangeBy(float value)
        {
            if (value >= MaxValue)
            {
                _currentHealth = MaxValue;
                RaiseHealthChanged();
                return;
            }
            
            _currentHealth += value;
            RaiseHealthChanged();
            
            if (value < 0f)
                RaiseDamaged();
            else if (value > 0f)
                RaiseHealed();
            
            if (_currentHealth <= 0f)
                RaiseHealthGone();
        }
    }
}