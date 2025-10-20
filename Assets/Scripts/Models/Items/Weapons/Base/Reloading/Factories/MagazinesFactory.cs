using Core.Input.Interfaces;
using Models.Items.Bullets.Base.Providers;
using Models.Items.Weapons.Base.Reloading.Base;
using Models.Items.Weapons.Base.Reloading.Enums;
using Models.Items.Weapons.Base.StatsHandling;

namespace Models.Items.Weapons.Base.Reloading.Factories
{
    public class MagazinesFactory
    {
        private readonly IInputHandler _inputHandler;
        
        public MagazinesFactory(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public Magazine Create(
            ReloadType reloadType,
            WeaponStatsCalculator statsCalculator,
            BulletsProvider bulletsProvider)
        {
            return reloadType switch
            {
                ReloadType.ByInput => new ByInputControlledMagazine(statsCalculator, bulletsProvider, _inputHandler),
                _ => throw new System.ArgumentOutOfRangeException(nameof(reloadType), reloadType, null)
            };
        }
    }
}