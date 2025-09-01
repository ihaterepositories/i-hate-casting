using Models.Creatures.Base.StatsHandling;
using Models.Items.Weapons.Base.StatsHandling;
using Zenject;

namespace Core.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CreatureStatsMultipliersProvider>().FromComponentInHierarchy().AsSingle();
            Container.Bind<WeaponStatsMultipliersProvider>().FromComponentInHierarchy().AsSingle();
        }
    }
}