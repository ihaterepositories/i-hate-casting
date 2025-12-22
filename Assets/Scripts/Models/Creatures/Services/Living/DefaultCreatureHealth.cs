using Models.Creatures.Services.Living.Base;
using Models.Creatures.Services.Living.Interfaces;
using Models.Creatures.Services.StatsCalculating.Interfaces;

namespace Models.Creatures.Services.Living
{
    public class DefaultCreatureHealth : CreatureHealth, ICreatureHealth
    {
        public DefaultCreatureHealth(ICreatureStatsCalculator statsCalculateService) : base(statsCalculateService)
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