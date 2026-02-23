using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.AssetsLoading.PrefabsProviders.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.AssetsLoading.PrefabsProviders
{
    public class AddressablesAssetsLoader : IAssetsLoader
    {
        private readonly Dictionary<AssetReferenceGameObject, GameObject> _cache = new();

        public async Task<GameObject> LoadAssetAsync(
            AssetReferenceGameObject reference,
            bool useCaching)
        {
            if (reference == null)
                throw new ArgumentNullException(nameof(reference));

            // If cached - return
            if (_cache.TryGetValue(reference, out var cachedPrefab))
                return cachedPrefab;

            // Loading
            var handle = reference.LoadAssetAsync<GameObject>();
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception($"Failed to load prefab from Addressables: {reference.RuntimeKey}");

            var prefab = handle.Result;

            if (!useCaching)
            {
                Addressables.Release(handle);
                return prefab;
            }
            else // caching
            {
                _cache[reference] = prefab;
                return prefab;
            }
        }

        public void CleanResources()
        {
            foreach (var pair in _cache)
            {
                Addressables.Release(pair.Key);
            }

            _cache.Clear();
        }
    }
}