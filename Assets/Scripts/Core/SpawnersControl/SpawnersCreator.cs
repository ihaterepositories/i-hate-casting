using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.AssetsLoaders.Interfaces;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.SpawnersControl.Dtos;
using Core.SpawnersControl.Interfaces;
using Models.Bullets;
using Models.Bullets.Enums;
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
        [SerializeField] private List<SpawnerCreationData<BulletType>> _bulletSpawnersData;
        
        [Header("Autospawners configuration")]
        
        private IAssetsLoader _assetsLoader;
        private SpawnersFactory _spawnersFactory;
        private IEventBusInvoker _eventBusInvoker;

        [Inject]
        private void Construct(
            IAssetsLoader assetsLoader,
            SpawnersFactory spawnersFactory,
            IEventBusInvoker eventBusInvoker)
        {
            _assetsLoader = assetsLoader;
            _spawnersFactory = spawnersFactory;
            _eventBusInvoker = eventBusInvoker;
        }

        private void OnDestroy()
        {
            _assetsLoader.CleanResources();
        }

        public async Task CreateAsync()
        {
            // Bullet spawners creating
            var bulletSpawners = new Dictionary<BulletType, ISpawner<Bullet>>();
            foreach (var sd in _bulletSpawnersData)
            {
                await CreateSpawner<Bullet, BulletType>(
                    sd, spawner => bulletSpawners.Add(sd.prefabType, spawner));
            }
            _eventBusInvoker.Invoke(new BulletSpawnersInitializedSignal(bulletSpawners));
            
        }

        private async Task CreateSpawner<T, TType>(
            SpawnerCreationData<TType> spawnerCreationData,
            Action<ISpawner<T>> onCreated)
        where T : PoolableMonoBehaviour
        where TType : Enum
        {
            var prefab = await _assetsLoader.LoadAssetAsync(spawnerCreationData.PrefabToSpawn, true);
            onCreated?.Invoke(_spawnersFactory.CreateSpawner<T>(prefab, spawnerCreationData.InstantiatingType));
        }
    }
}