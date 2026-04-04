using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.AssetsLoaders.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.AssetsLoaders
{
    public class CachingAssetsLoader : IAssetsLoader, IAssetsProvider
    {
        private readonly Dictionary<AssetReferenceGameObject, AsyncOperationHandle<GameObject>> _cache = new();

        public IEnumerator LoadAssetCoroutine(AssetReferenceGameObject assetReference)
        {
            if (_cache.ContainsKey(assetReference))
                yield break;

            var handle = assetReference.LoadAssetAsync<GameObject>();
            yield return handle;

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception($"Failed to load prefab: {assetReference.RuntimeKey}");

            _cache.Add(assetReference, handle);
        }

        public GameObject GetAsset(AssetReferenceGameObject assetReference)
        {
            if (_cache.TryGetValue(assetReference, out var handle))
                return handle.Result;

            throw new Exception($"Asset not found: {assetReference.RuntimeKey}");
        }

        public void ReleaseCachedAsset(AssetReferenceGameObject assetReference)
        {
            if (_cache.TryGetValue(assetReference, out var handle))
            {
                Addressables.Release(handle);
                _cache.Remove(assetReference);
            }
            else
            {
                Debug.LogWarning($"Trying to release non-cached asset: {assetReference.RuntimeKey}");
            }
        }

        public void CleanResources()
        {
            foreach (var handle in _cache.Values)
            {
                Addressables.Release(handle);
            }

            _cache.Clear();
        }
    }
}