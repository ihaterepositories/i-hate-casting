using Models.Creatures.Implementations.PlayerImplementation.StatsMultipliers;
using Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation.StatsMultipliers;
using UserInterfaceUtils.Animators;
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

            Container.Bind<ScreenBorderAnimator>().FromComponentInHierarchy().AsSingle();
        }
    }
}