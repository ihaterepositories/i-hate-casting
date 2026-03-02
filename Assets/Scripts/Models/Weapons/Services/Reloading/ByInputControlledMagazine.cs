using Core.Input.Interfaces;
using Models.Bullets;
using Models.Bullets.Dtos;
using Models.Weapons.Services.Reloading.Base;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using Spawners.Interfaces;

namespace Models.Weapons.Services.Reloading
{
    public class ByInputControlledMagazine : Magazine
    {
        private readonly IInputHandler _inputHandler;
        
        public ByInputControlledMagazine(
            ISpawner<Bullet> bulletsSpawner,
            IWeaponStatsCalculator statsCalculator,
            IInputHandler inputHandler) : base(bulletsSpawner, statsCalculator)
        {
            _inputHandler = inputHandler;
        }

        protected override bool ReloadRule()
        {
            return _inputHandler.IsReloadButtonPressed();
        }
    }
}