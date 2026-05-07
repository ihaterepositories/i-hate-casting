using Core.AssetsLoaders;
using Core.AssetsLoaders.Interfaces;
using Core.GameEventsControl;
using Core.GameEventsControl.Interfaces;
using Core.Pausing;
using Core.Pausing.Interfaces;
using Core.RoundBootstrapControl;
using Core.RoundBootstrapControl.Interfaces;
using Core.SpawnersControl;
using Core.SpawnersControl.Interfaces;
using Core.TimerEngines;
using Core.TimerEngines.Interfaces;
using Models.Bullets.Services.LifeTimeCalculating.Factories;
using Models.Bullets.Services.Moving.Factories;
using Models.Creatures.Services.Animators.Factories;
using Models.Creatures.Services.Destroyers.Factories;
using Models.Creatures.Services.Health.Factories;
using Models.Creatures.Services.MoveBoosters.Factories;
using Models.Creatures.Services.Movers.Factories;
using Models.Creatures.Services.StatsScalers.Providers;
using Models.Interactables.Base.Visuals;
using Models.UI.CooldownViewBars.Services.ValueProviding.Factories;
using Models.UI.QuantityViewBars.Services.ValueProviding.Factories;
using Models.UI.QuantityViewBars.Services.Visualizing.Factories;
using Models.UI.StatusTexts.Services.ValueProviding.Factories;
using Models.UI.StatusTexts.Services.Visualizing.Factories;
using Models.Weapons.Services.Aimers.Factories;
using Models.Weapons.Services.Reloaders.Factories;
using Models.Weapons.Services.Shooters.Factories;
using Models.Weapons.Services.StatsScalers.Providers;
using ObjectPools.Factories;
using Shared.Services.ObstaclesBypassCalculators.Factories;
using Spawners;
using Spawners.Factories;
using Spawners.Services.Instantiaters.Factories;
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
            
            Container.Bind<ITimerEngine>().To<CachedTimerEngine>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<EventBus>().AsSingle();
            
            Container.Bind<InstantiatersFactory>().AsSingle().NonLazy();
            Container.Bind<SpawnPositionCalculatorsFactory>().AsSingle().NonLazy();
            Container.Bind<SpawnersFactory>().AsSingle().NonLazy();
            
            Container.Bind<ISpawnersCreator>().To<SpawnersCreator>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RoundBootstrapper>().FromComponentsInHierarchy().AsSingle();
            
            Container.Bind<CreatureStatsScalersProvider>().AsSingle().NonLazy();
            Container.Bind<WeaponStatsScalersProvider>().AsSingle().NonLazy();
            
            Container.Bind<ObjectPoolsFactory>().AsSingle().NonLazy();
            
            Container.Bind<BulletMoversFactory>().AsSingle().NonLazy();
            Container.Bind<BulletLifeTimeCalculatorsFactory>().AsSingle().NonLazy();
            
            Container.Bind<WeaponReloadersFactory>().AsSingle().NonLazy();
            Container.Bind<WeaponShootersFactory>().AsSingle().NonLazy();
            Container.Bind<WeaponAimersFactory>().AsSingle().NonLazy();

            Container.Bind<CreatureHealthesFactory>().AsSingle().NonLazy();
            Container.Bind<CreatureMoversFactory>().AsSingle().NonLazy();
            Container.Bind<ObstaclesBypassCalculatorsFactory>().AsSingle().NonLazy();
            Container.Bind<CreatureMoveBoostersFactory>().AsSingle().NonLazy();
            Container.Bind<CreatureDestroyersFactory>().AsSingle().NonLazy();
            Container.Bind<CreatureAnimatiorsFactory>().AsSingle().NonLazy();

            Container.Bind<ItemsSpawner>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<StatusTextValueProvidersFactory>().AsSingle().NonLazy();
            Container.Bind<StatusTextVisualizersFactory>().AsSingle().NonLazy();
            
            Container.Bind<QuantityViewBarValueProvidersFactory>().AsSingle().NonLazy();
            Container.Bind<QuantityViewBarVisualizersFactory>().AsSingle().NonLazy();
            
            Container.Bind<CooldownViewBarValueProvidersFactory>().AsSingle().NonLazy();
            
            Container.Bind<CountdownVisualizersFactory>().AsSingle().NonLazy();
            Container.Bind<ImageFadeAnimatorsFactory>().AsSingle().NonLazy();
            Container.Bind<OnCanInteractHintText>().FromComponentInHierarchy().AsSingle();
        }
    }
}