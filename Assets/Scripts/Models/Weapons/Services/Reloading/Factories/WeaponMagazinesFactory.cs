using Core.Input.Interfaces;
using Models.Bullets;
using Models.Bullets.Dtos;
using Models.Weapons.Services.Reloading.Base;
using Models.Weapons.Services.Reloading.Enums;
using Models.Weapons.Services.StatsCalculating.Interfaces;

namespace Models.Weapons.Services.Reloading.Factories
{
    public class WeaponMagazinesFactory
    {
        private readonly IGameObjectFactory _bulletsFactory;
        private readonly IConfigsProvider _configsProvider;
        private readonly IInputHandler _inputHandler;
        
        public WeaponMagazinesFactory(GameObjectFactoriesFactory gameObjectFactoriesFactory, IInputHandler inputHandler)
        {
            _bulletsFactory = gameObjectFactoriesFactory.GetFor<Bullet, BulletConfig>();
            _inputHandler = inputHandler;
        }
        
        public Magazine Create(
            BulletConfigKey bulletConfigKey,
            ReloadType reloadType,
            IWeaponStatsCalculator statsCalculator)
        {
            return reloadType switch
            {
                ReloadType.ByInput => new ByInputControlledMagazine(_configsProvider.GetFor<BulletConfig>(bulletConfigKey.ToString()), statsCalculator, _bulletsFactory, _inputHandler),
                _ => throw new System.ArgumentOutOfRangeException(nameof(reloadType), reloadType, null)
            };
        }
    }
}