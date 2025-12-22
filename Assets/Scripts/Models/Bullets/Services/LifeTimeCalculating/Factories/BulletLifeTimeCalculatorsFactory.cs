using System;
using Models.Bullets.Services.LifeTimeCalculating.Enums;
using Models.Bullets.Services.LifeTimeCalculating.Interfaces;

namespace Models.Bullets.Services.LifeTimeCalculating.Factories
{
    public class BulletLifeTimeCalculatorsFactory
    {
        public IBulletLifeTimeCalculateService Create(
            BulletLifeTimeCalculatorType lifeTimeCalculatorType, 
            float bulletMoveSpeed, 
            float moveRange)
        {
            return lifeTimeCalculatorType switch
            {
                BulletLifeTimeCalculatorType.Default => new BulletLifeTimeCalculator(bulletMoveSpeed, moveRange),
                _ => throw new ArgumentOutOfRangeException(nameof(lifeTimeCalculatorType), lifeTimeCalculatorType, null)
            };
        }
    }
}