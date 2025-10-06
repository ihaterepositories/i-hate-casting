using Core.GameControl;
using Models.Items.Base.Spawners;
using Models.Items.Weapons.Base.StatsHandling;
using Models.Items.Weapons.Bullets.EnemyBulletImpl.Pools;
using Models.Items.Weapons.Bullets.PlayerBulletImpl.Pools;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using Models.WorldObjects.Creatures.Base.Visuals.Pools;
using Models.WorldObjects.Creatures.PlayerImpl;
using Models.WorldObjects.Interactables.Base.Visuals;
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

            Container.Bind<GamePauser>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            Container.Bind<ItemsSpawner>().FromComponentInHierarchy().AsSingle();

            Container.Bind<OnCanInteractHintText>().FromComponentInHierarchy().AsSingle();
        }
    }
}