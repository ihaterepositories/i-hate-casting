using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.AssetsLoaders.Interfaces
{
    public interface IAssetsProvider
    {
        /// <summary>
        /// Returns loaded asset from cache.
        /// </summary>
        /// <param name="assetReference">Addressables asset reference.</param>
        /// <returns></returns>
        public GameObject GetAsset(AssetReferenceGameObject assetReference);
    }
}