using Models.Weapons.Data.WeaponStatsMultipliers;
using Zenject;

namespace Core.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerShortRangeWeaponStatsMultiplier>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerMediumRangeWeaponStatsMultiplier>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerLongRangeWeaponStatsMultiplier>().FromComponentInHierarchy().AsSingle();
        }
    }
}