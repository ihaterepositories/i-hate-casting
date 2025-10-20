using Core.Input.Interfaces;
using Models.Items.Bullets.Base.Providers;
using Models.Items.Weapons.Base.Reloading.Base;
using Models.Items.Weapons.Base.StatsHandling;

namespace Models.Items.Weapons.Base.Reloading
{
    public class ByInputControlledMagazine : Magazine
    {
        private readonly IInputHandler _inputHandler;
        
        public ByInputControlledMagazine(
            WeaponStatsCalculator statsCalculator,
            BulletsProvider bulletsProvider,
            IInputHandler inputHandler) : base(statsCalculator, bulletsProvider)
        {
            _inputHandler = inputHandler;
        }

        protected override bool ReloadRule()
        {
            return _inputHandler.IsReloadButtonPressed();
        }
    }
}