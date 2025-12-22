using System;
using System.Threading.Tasks;
using Models.Bullets.Services.LifeTimeCalculating.Interfaces;

namespace Models.Bullets.Services.LifeTimeCalculating
{
    public class BulletLifeTimeCalculator : IBulletLifeTimeCalculateService
    {
        private float _lifeTimeLimit;

        public BulletLifeTimeCalculator(float moveSpeed, float range)
        {
            _lifeTimeLimit = range / moveSpeed;
        }

        public event Action OnTimeLimitReached;
        
        public void UpdateLifeTimeLimit(float moveSpeed, float moveRange)
        {
            _lifeTimeLimit = moveRange / moveSpeed;
        }
        
        public void StartCalculate()
        {
            _ = ReduceLifeTimeAsync();
        }

        private async Task ReduceLifeTimeAsync()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(_lifeTimeLimit));
            }
            finally
            {
                OnTimeLimitReached?.Invoke();
            }
        }
    }
}