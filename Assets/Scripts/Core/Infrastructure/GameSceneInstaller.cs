using Core.Pausing;
using Core.Pausing.Interfaces;
using Models.Bullets.Services.LifeTimeCalculating.Factories;
using Models.Bullets.Services.Moving.Factories;
using Models.Creatures.Services.Animating.Factories;
using Models.Creatures.Services.Destroying.Factories;
using Models.Creatures.Services.Living.Factories;
using Models.Creatures.Services.MoveBoosting.Factories;
using Models.Creatures.Services.Moving.Factories;
using Models.Creatures.Services.ObstaclesBypassing.Factories;
using Models.Creatures.Services.StatsCalculating.Factories;
using Models.Creatures.Services.StatsCalculating.StatsModifying.Providers;
using Models.Interactables.Base.Visuals;
using Models.UI.CooldownViewBars.Services.ValueProviding.Factories;
using Models.UI.QuantityViewBars.Services.ValueProviding.Factories;
using Models.UI.QuantityViewBars.Services.Visualizing.Factories;
using Models.UI.StatusTexts.Services.ValueProviding.Factories;
using Models.UI.StatusTexts.Services.Visualizing.Factories;
using Models.Weapons.Services.Aiming.Factories;
using Models.Weapons.Services.Reloading.Factories;
using Models.Weapons.Services.Shooting.Factories;
using Models.Weapons.Services.StatsCalculating.Factories;
using Models.Weapons.Services.StatsModifying.Providers;
using Pooling.Factories;
using Spawners;
using Spawners.Factories;
using Spawners.Services.Instantiaters.Factories;
using Spawners.Services.SpawnBehaviourProviders.Factories;
using Spawners.Services.SpawnPositionCalculators.Dtos;
using Spawners.Services.SpawnPositionCalculators.Factories;
using UIServices.CountdownVisualizers.Factories;
using UIServices.ImageFadeAnimators.Factories;
using Zenject;

namespace Core.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SafeSpawnSettings>().FromScriptableObjectResource("SafeSpawnSettings").AsSingle().NonLazy();
            
            Container.Bind<IPauser>().To<Pauser>().AsSingle().NonLazy();
            
            Container.Bind<CreatureStatsModifiersProvider>().AsSingle().NonLazy();
            Container.Bind<CreatureStatsCalculatorsFactory>().AsSingle().NonLazy();
            
            Container.Bind<WeaponStatsModifiersProvider>().AsSingle().NonLazy();
            Container.Bind<WeaponStatsCalculatorsFactory>().AsSingle().NonLazy();
            
            Container.Bind<ObjectPoolsFactory>().AsSingle().NonLazy();
            
            Container.Bind<BulletMoversFactory>().AsSingle().NonLazy();
            Container.Bind<BulletLifeTimeCalculatorsFactory>().AsSingle().NonLazy();
            
            Container.Bind<WeaponMagazinesFactory>().AsSingle().NonLazy();
            Container.Bind<WeaponShootersFactory>().AsSingle().NonLazy();
            Container.Bind<WeaponAimersFactory>().AsSingle().NonLazy();

            Container.Bind<CreatureHealthServicesFactory>().AsSingle().NonLazy();
            Container.Bind<CreatureMoversFactory>().AsSingle().NonLazy();
            Container.Bind<CreatureObstaclesBypassersFactory>().AsSingle().NonLazy();
            Container.Bind<CreatureMoveBoostersFactory>().AsSingle().NonLazy();
            Container.Bind<CreatureDestroyersFactory>().AsSingle().NonLazy();
            Container.Bind<CreatureAnimationLaunchersFactory>().AsSingle().NonLazy();

            Container.Bind<InstantiatersFactory>().AsSingle().NonLazy();
            Container.Bind<SpawnPositionCalculatorsFactory>().AsSingle().NonLazy();
            Container.Bind<SpawnBehaviourProvidersFactory>().AsSingle().NonLazy();
            Container.Bind<SpawnersFactory>().AsSingle().NonLazy();

            Container.Bind<ItemsSpawner>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<ResourcesCleaner>().AsSingle().NonLazy();
            Container.Bind<GameBootstrapper>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<StatusTextValueProvidersFactory>().AsSingle().NonLazy();
            Container.Bind<StatusTextVisualizersFactory>().AsSingle().NonLazy();
            
            Container.Bind<QuantityVewBarValueProvidersFactory>().AsSingle().NonLazy();
            Container.Bind<QuantityViewBarVisualizersFactory>().AsSingle().NonLazy();
            
            Container.Bind<CooldownViewBarValueProvidersFactory>().AsSingle().NonLazy();
            
            Container.Bind<CountdownVisualizersFactory>().AsSingle().NonLazy();
            Container.Bind<ImageFadeAnimatorsFactory>().AsSingle().NonLazy();
            Container.Bind<OnCanInteractHintText>().FromComponentInHierarchy().AsSingle();
        }
    }
}