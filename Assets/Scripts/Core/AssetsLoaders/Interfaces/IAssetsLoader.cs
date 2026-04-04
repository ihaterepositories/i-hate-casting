using System.Collections;
using System.Threading.Tasks;
using Shared.Systems.ResourcesCleaning.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.AssetsLoaders.Interfaces
{
    public interface IAssetsLoader : IResourceCleanable
    {
        /// <summary>
        /// Loads asset from Addressables and cache it.
        /// If asset was already cached - loading will not happen.
        /// </summary>
        /// <param name="assetReference">Asset reference in Addressables.</param>
        public IEnumerator LoadAssetCoroutine(AssetReferenceGameObject assetReference);
    }
}