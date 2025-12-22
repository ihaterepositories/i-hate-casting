using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.AssetsLoading.PrefabsProviders.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.AssetsLoading.PrefabsProviders
{
    public class AddressablesPrefabsProvider : IPrefabsProvider
    {
        private readonly Dictionary<AssetReferenceGameObject, GameObject> _loadedPrefabs = new ();
        private readonly HashSet<AssetReferenceGameObject> _unreleasedReferences = new();
        
        public async Task<GameObject> GetPrefabAsync(AssetReferenceGameObject reference, CancellationToken cancellationToken)
        {
            if (reference == null)
                throw new ArgumentNullException(nameof(reference));

            // If prefab was loaded before, it returns
            if (_loadedPrefabs.TryGetValue(reference, out var gameObject))
                return gameObject;
            
            var handle = reference.LoadAssetAsync<GameObject>();

            // Register what to do if operation cancelled by token
            using (cancellationToken.Register(() =>
                         {
                             if (handle.IsValid())
                                 Addressables.Release(handle);
                         }))
            {
                await handle.Task;

                // Run registered code and exit from method if cancellation requested
                cancellationToken.ThrowIfCancellationRequested();
                
                _unreleasedReferences.Add(reference);
                
                // Caching prefabs
                if (!_loadedPrefabs.ContainsKey(reference))
                    _loadedPrefabs.Add(reference, handle.Result);
                
                return handle.Result;
            }
        }

        public void CleanResources()
        {
            foreach (var reference in _unreleasedReferences)
            {
                Addressables.Release(reference);
            }
            
            _unreleasedReferences.Clear();
        }
    }
}