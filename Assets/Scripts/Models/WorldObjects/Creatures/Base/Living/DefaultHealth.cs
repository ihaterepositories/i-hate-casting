using Models.WorldObjects.Creatures.Base.StatsHandling;

namespace Models.WorldObjects.Creatures.Base.Living
{
    public class DefaultHealth : Health
    {
        public DefaultHealth(CreatureStatsCalculator statsCalculator) : base(statsCalculator)
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