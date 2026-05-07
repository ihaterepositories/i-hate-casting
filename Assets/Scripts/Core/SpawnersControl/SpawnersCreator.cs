using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.AssetsLoaders.Interfaces;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.SpawnersControl.Dtos;
using Core.SpawnersControl.Interfaces;
using Models.Bullets;
using Models.Bullets.Enums;
using Models.Creatures;
using Models.Creatures.Enums;
using Shared.Models.PoolableMonoBehaviours;
using Spawners.Factories;
using Spawners.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.SpawnersControl
{
    [DisallowMultipleComponent]
    public class SpawnersCreator : MonoBehaviour, ISpawnersCreator
    {
        [Header("Spawners configuration")]
        [SerializeField] private SpawnerCreationData _playerBulletSD;
        [SerializeField] private SpawnerCreationData _enemyBulletSD;
        [SerializeField] private SpawnerCreationData _playerSD;
        
        // Services
        private IAssetsLoader _assetsLoader;
        private IAssetsProvider _assetsProvider;
        private SpawnersFactory _spawnersFactory;
        private IEventBusInvoker _eventBusInvoker;

        public Dictionary<CreatureType, ISpawner<Creature>> CreatureSpawners { get; private set; }
 
        [Inject]
        private void Construct(
            IAssetsLoader assetsLoader,
            IAssetsProvider assetsProvider,
            SpawnersFactory spawnersFactory,
            IEventBusInvoker eventBusInvoker)
        {
            _assetsLoader = assetsLoader;
            _assetsProvider = assetsProvider;
            _spawnersFactory = spawnersFactory;
            _eventBusInvoker = eventBusInvoker;
        }

        public IEnumerator CreateCoroutine()
        {
            // Bullet spawners creating
            // var bulletSpawners = new Dictionary<BulletType, ISpawner<Bullet>>();
            //
            // yield return CreateSpawner<Bullet>(_playerBulletSD, spawner => bulletSpawners[BulletType.PlayerBullet] = spawner);
            // yield return CreateSpawner<Bullet>(_enemyBulletSD,  spawner => bulletSpawners[BulletType.EnemyBullet] = spawner);
            //
            // _eventBusInvoker.Invoke(new BulletSpawnersInitializedSignal(bulletSpawners));
            
            // Creature spawners creating
            var creatureSpawners = new Dictionary<CreatureType, ISpawner<Creature>>();
            
            yield return CreateSpawner<Creature>(_playerSD, spawner => creatureSpawners[CreatureType.Player] = spawner);
            
            CreatureSpawners = creatureSpawners;
            _eventBusInvoker.Invoke(new CreatureSpawnersInitializedSignal(creatureSpawners));
        }

        private IEnumerator CreateSpawner<T>(
            SpawnerCreationData spawnerCreationData,
            Action<ISpawner<T>> onCreated)
            where T : PoolableMonoBehaviour
        {
            yield return _assetsLoader.LoadAssetCoroutine(spawnerCreationData.PrefabToSpawn);
            var prefab = _assetsProvider.GetAsset(spawnerCreationData.PrefabToSpawn);
            
            var spawner = _spawnersFactory.CreateSpawner<T>(
                prefab, 
                spawnerCreationData.InstantiatingType, 
                spawnerCreationData.SpawnPositionType);
            
            onCreated?.Invoke(spawner);
        }
    }
}