using System.Threading;
using System.Threading.Tasks;
using Systems.ResourcesCleaning.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.AssetsLoading.PrefabsProviders.Interfaces
{
    public interface IPrefabsProvider : IResourceCleanable
    {
        public Task<GameObject> GetPrefabAsync(AssetReferenceGameObject reference, CancellationToken cancellationToken);
    }
}