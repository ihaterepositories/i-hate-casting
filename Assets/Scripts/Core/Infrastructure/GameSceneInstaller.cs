using Models.Creatures.Base.StatsHandling;
using Models.Creatures.Implementations.PlayerImplementation;
using Models.Items.Weapons.Base.StatsHandling;
using Models.Items.Weapons.Bullets.Implementations.PlayerBulletImplementation.Pools;
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
            
            Container.Bind<Player>().FromInstance(_player).AsSingle();
        }
    }
}