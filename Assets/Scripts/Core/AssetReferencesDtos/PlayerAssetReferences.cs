using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.AssetReferencesDtos
{
    [CreateAssetMenu(fileName = "PlayerAssetReference", menuName = "ScriptableObjects/AssetReferencesDtos/PlayerAssetReference")]
    public class PlayerAssetReferences : ScriptableObject
    {
        public readonly AssetReferenceGameObject PlayerPrefab;
    }
}