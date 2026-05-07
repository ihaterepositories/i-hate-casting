using System.Collections;
using System.Threading.Tasks;
using Shared.Systems.ResourcesCleaning.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.AssetsLoaders.Interfaces
{
    public interface IAssetsLoader
    {
        /// <summary>
        /// Loads asset from Addressables and cache it.
        /// If asset was already cached - loading will not happen.
        /// </summary>
        /// <param name="assetReference">Asset reference in Addressables.</param>
        /// <param name="isLocal">Determines if asset will be cached as a local scene one or a global one.</param>
        public IEnumerator LoadAssetCoroutine(AssetReferenceGameObject assetReference, bool isLocal = true);
    }
}