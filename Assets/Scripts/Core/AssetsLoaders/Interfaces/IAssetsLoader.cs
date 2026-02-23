using System.Threading;
using System.Threading.Tasks;
using Systems.ResourcesCleaning.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.AssetsLoading.PrefabsProviders.Interfaces
{
    public interface IAssetsLoader : IResourceCleanable
    {
        /// <summary>
        /// Loads asset from Addressables.
        /// If asset already loaded and cached - it returns from cache.
        /// </summary>
        /// <param name="reference">Asset reference in Addressables</param>
        /// <param name="useCaching">Keep it true to cache loaded asset</param>
        /// <returns></returns>
        public Task<GameObject> LoadAssetAsync(AssetReferenceGameObject reference, bool useCaching);
    }
}