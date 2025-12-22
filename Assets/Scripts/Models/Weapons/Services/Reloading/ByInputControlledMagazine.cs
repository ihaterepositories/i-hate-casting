using Core.Input.Interfaces;
using Models.Bullets.Dtos;
using Models.Weapons.Services.Reloading.Base;
using Models.Weapons.Services.StatsCalculating.Interfaces;

namespace Models.Weapons.Services.Reloading
{
    public class ByInputControlledMagazine : Magazine
    {
        private readonly IInputHandler _inputHandler;
        
        public ByInputControlledMagazine(
            BulletConfig bulletConfig,
            IWeaponStatsCalculator statsCalculator,
            IGameObjectFactory bulletsFactory,
            IInputHandler inputHandler) : base(bulletConfig, statsCalculator, bulletsFactory)
        {
            _inputHandler = inputHandler;
        }

        protected override bool ReloadRule()
        {
            return _inputHandler.IsReloadButtonPressed();
        }
    }
}