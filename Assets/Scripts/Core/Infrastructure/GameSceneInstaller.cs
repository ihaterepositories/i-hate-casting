using Models.Creatures.Base.StatsHandling;
using Models.Creatures.Implementations.EnemyImplementation.Visuals.Pools;
using Models.Creatures.Implementations.PlayerImplementation;
using Models.Creatures.Items.Implementations.Artefacts.Base.Spawners;
using Models.Creatures.Items.Implementations.Weapons.Base.StatsHandling;
using Models.Creatures.Items.Implementations.Weapons.Bullets.Implementations.EnemyBulletImplementation.Pools;
using Models.Creatures.Items.Implementations.Weapons.Bullets.Implementations.PlayerBulletImplementation.Pools;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        
        public override void InstallBindings()
        {
            Container.Bind<CreatureStatsMultipliersProvider>().FromComponentInHierarchy().AsSingle();
            Container.Bind<WeaponStatsMultipliersProvider>().FromComponentInHierarchy().AsSingle();

            Container.Bind<PlayerBulletsPool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyBulletsPool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<OnDeathExplosionEffectsPool>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            Container.Bind<ArtefactsSpawner>().FromComponentInHierarchy().AsSingle();
        }
    }
}