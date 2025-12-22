using UnityEngine;

namespace Spawners.Services.SpawnPositionCalculators.Dtos
{
    [CreateAssetMenu(fileName = "SafeSpawnSettings", menuName = "ScriptableObjects/Settings/SafeSpawnSettings")]
    public class SafeSpawnSettings : ScriptableObject
    {
        [SerializeField] private Vector2 _spawnAreaSize = new(24f, 12f);
        [SerializeField] private float _minDistanceFromOtherCollidersWhenSpawn = 3f;
        
        public Vector2 SpawnAreaSize => _spawnAreaSize;
        public float MinDistanceFromOtherCollidersWhenSpawn => _minDistanceFromOtherCollidersWhenSpawn;
    }
}