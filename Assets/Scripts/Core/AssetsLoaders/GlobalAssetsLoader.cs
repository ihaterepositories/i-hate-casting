using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.AssetsLoaders.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.AssetsLoaders
{
    public class GlobalAssetsLoader : IAssetsLoader, IAssetsProvider, IInitializable
    {
        private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _globalCachedHandles = new();
        private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _localCachedHandles = new();

        public void Initialize()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            ReleaseLocalAssets();
        }
        
        public IEnumerator LoadAssetCoroutine(AssetReferenceGameObject assetReference, bool isLocal = true)
        {
            var key = assetReference.RuntimeKey.ToString();

            if (_localCachedHandles.ContainsKey(key) || _globalCachedHandles.ContainsKey(key))
                yield break;

            var handle = assetReference.LoadAssetAsync<GameObject>();
            
            if (isLocal)
                _localCachedHandles[key] = handle;
            else
                _globalCachedHandles[key] = handle;

            yield return handle;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Cannot load asset: {assetReference.RuntimeKey}.");
                if (isLocal)
                    _localCachedHandles.Remove(key);
                else
                    _globalCachedHandles.Remove(key);
            }
        }

        public GameObject GetAsset(AssetReferenceGameObject assetReference)
        {
            if (_localCachedHandles.TryGetValue(assetReference.RuntimeKey.ToString(), out var handle))
            {
                if (!handle.IsDone || handle.Status != AsyncOperationStatus.Succeeded)
                    throw new Exception($"Cannot get asset {assetReference.RuntimeKey}. Handle failed or unfinished.");
                
                return handle.Result;
            }
            
            if (_globalCachedHandles.TryGetValue(assetReference.RuntimeKey.ToString(), out handle))
            {
                if (!handle.IsDone || handle.Status != AsyncOperationStatus.Succeeded)
                    throw new Exception($"Cannot get asset {assetReference.RuntimeKey}. Handle failed or unfinished.");
                
                return handle.Result;
            }

            throw new Exception($"Asset not found: {assetReference.RuntimeKey}");
        }

        private void ReleaseLocalAssets()
        {
            foreach (var handle in _localCachedHandles.Values)
            {
                Addressables.Release(handle);
            }

            _localCachedHandles.Clear();
        }
    }
}