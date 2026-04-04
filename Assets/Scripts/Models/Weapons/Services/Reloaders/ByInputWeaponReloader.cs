using Core.Input.Interfaces;
using Core.TimerEngines.Interfaces;
using Models.Bullets;
using Models.Weapons.Dtos;
using Models.Weapons.Services.Reloaders.Base;
using Models.Weapons.Services.StatsScalers.Interfaces;
using Spawners.Interfaces;

namespace Models.Weapons.Services.Reloaders
{
    public class ByInputWeaponReloader : WeaponReloader
    {
        private readonly IInputHandler _inputHandler;
        
        public ByInputWeaponReloader(
            WeaponStats stats,
            IWeaponStatsScaler statsScaler,
            ISpawner<Bullet> bulletsSpawner,
            IInputHandler inputHandler,
            ITimerEngine timerEngine) : base(stats, statsScaler, bulletsSpawner, timerEngine)
        {
            _inputHandler = inputHandler;
        }

        protected override bool IsReloadRuleComplied()
        {
            return _inputHandler.IsReloadButtonPressed();
        }
    }
}