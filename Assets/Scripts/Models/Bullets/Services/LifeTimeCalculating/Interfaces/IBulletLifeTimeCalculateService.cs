using System;

namespace Models.Bullets.Services.LifeTimeCalculating.Interfaces
{
    public interface IBulletLifeTimeCalculateService
    {
        public event Action OnTimeLimitReached;
        public void StartCalculate();
        public void UpdateLifeTimeLimit(float moveSpeed, float moveRange);
    }
}