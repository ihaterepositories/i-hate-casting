using Models.Creatures.Implementations.PlayerImplementation.StatsMultipliers;
using Models.Weapons.Implementations.PlayerWeaponImplementation.StatsMultipliers;
using Zenject;

namespace Core.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerStatsMultiplier>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<PlayerShortRangeWeaponStatsMultiplier>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerMediumRangeWeaponStatsMultiplier>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerLongRangeWeaponStatsMultiplier>().FromComponentInHierarchy().AsSingle();
        }
    }
}