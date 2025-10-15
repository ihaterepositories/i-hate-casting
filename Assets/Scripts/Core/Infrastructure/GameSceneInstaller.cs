using Core.GameControl;
using Core.Input.InputHandlers;
using Core.Input.Interfaces;
using Models.Items.Base.Spawners;
using Models.Items.Weapons.Base.StatsHandling;
using Models.Items.Weapons.Bullets.EnemyBulletImpl.Pools;
using Models.Items.Weapons.Bullets.PlayerBulletImpl.Pools;
using Models.WorldObjects.Creatures.Base.Animating.Pools;
using Models.WorldObjects.Creatures.Base.Living;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Fabrics;
using Models.WorldObjects.Creatures.Base.Moving.Fabrics;
using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Fabrics;
using Models.WorldObjects.Creatures.Base.StatsHandling.Fabrics;
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
            Container.Bind<CreatureStatsMultiplierFactory>().AsSingle().NonLazy();
            Container.Bind<WeaponStatsMultipliersProvider>().FromComponentInHierarchy().AsSingle();

            Container.Bind<IInputHandler>().To<KeyboardInputHandler>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IHealthService>().To<Health>().AsTransient();

            Container.Bind<PlayerPositionTracker>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ObstaclesBypassersFabric>().AsSingle().NonLazy();
            Container.Bind<MoversFabric>().AsSingle().NonLazy();
            Container.Bind<MoveBoostersFabric>().AsSingle().NonLazy();
            
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            Container.Bind<PlayerBulletsPool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyBulletsPool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<OnDeathExplosionEffectsPool>().FromComponentInHierarchy().AsSingle();

            Container.Bind<GamePauser>().FromComponentInHierarchy().AsSingle();

            Container.Bind<ItemsSpawner>().FromComponentInHierarchy().AsSingle();

            Container.Bind<OnCanInteractHintText>().FromComponentInHierarchy().AsSingle();
        }
    }
}