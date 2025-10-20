using Core.GameControl;
using Core.Input.InputHandlers;
using Core.Input.Interfaces;
using Models.Items.Base.Spawners;
using Models.Items.Bullets.Base.Providers;
using Models.Items.Bullets.EnemyBulletImpl.Pools;
using Models.Items.Bullets.PlayerBulletImpl.Pools;
using Models.Items.Weapons.Base.Aiming.Factories;
using Models.Items.Weapons.Base.Reloading.Factories;
using Models.Items.Weapons.Base.Shooting.Factories;
using Models.Items.Weapons.Base.StatsHandling.Providers;
using Models.WorldObjects.Creatures.Base.Animating.Pools;
using Models.WorldObjects.Creatures.Base.Living;
using Models.WorldObjects.Creatures.Base.Living.Factories;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Factories;
using Models.WorldObjects.Creatures.Base.Moving.Factories;
using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Factories;
using Models.WorldObjects.Creatures.Base.StatsHandling.Providers;
using Models.WorldObjects.Creatures.PlayerImpl;
using Models.WorldObjects.Creatures.PlayerImpl.DataContainers;
using Models.WorldObjects.Interactables.Base.Visuals;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Core.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_playerView")] [SerializeField] private Player _player;
        
        public override void InstallBindings()
        {
            Container.Bind<CreatureStatsMultipliersProvider>().AsSingle().NonLazy();
            Container.Bind<WeaponStatsMultipliersProvider>().AsSingle().NonLazy();

            Container.Bind<PlayerPositionTracker>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<IInputHandler>().To<KeyboardInputHandler>().AsSingle().NonLazy();
            
            Container.Bind<PlayerBulletsPool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyBulletsPool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BulletsProvider>().AsSingle().NonLazy();
            
            Container.Bind<MagazinesFactory>().AsSingle().NonLazy();
            Container.Bind<ShootersFactory>().AsSingle().NonLazy();
            Container.Bind<AimersFactory>().AsSingle().NonLazy();

            Container.Bind<HealthServicesFactory>().AsSingle().NonLazy();
            Container.Bind<ObstaclesBypassersFactory>().AsSingle().NonLazy();
            Container.Bind<MoversFactory>().AsSingle().NonLazy();
            Container.Bind<MoveBoostersFactory>().AsSingle().NonLazy();
            
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            
            Container.Bind<OnDeathExplosionEffectsPool>().FromComponentInHierarchy().AsSingle();

            Container.Bind<GamePauser>().FromComponentInHierarchy().AsSingle();

            Container.Bind<ItemsSpawner>().FromComponentInHierarchy().AsSingle();

            Container.Bind<OnCanInteractHintText>().FromComponentInHierarchy().AsSingle();
        }
    }
}