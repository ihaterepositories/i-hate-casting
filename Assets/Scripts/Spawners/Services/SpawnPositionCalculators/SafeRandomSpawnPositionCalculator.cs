using Spawners.Services.SpawnPositionCalculators.Dtos;
using Spawners.Services.SpawnPositionCalculators.Interfaces;
using UnityEngine;

namespace Spawners.Services.SpawnPositionCalculators
{
    public class SafeRandomSpawnPositionCalculator : ISpawnPositionCalculator
    {
        private readonly SafeSpawnSettings _settings;

        public SafeRandomSpawnPositionCalculator(SafeSpawnSettings settings)
        {
            _settings = settings;
        }

        public Vector3 GetSpawnPosition()
        {
            // Generate a random position within the spawn radius
            var spawnPosition = new Vector2(
                Random.Range(-_settings.SpawnAreaSize.x, _settings.SpawnAreaSize.x),
                Random.Range(-_settings.SpawnAreaSize.y, _settings.SpawnAreaSize.y));

            // 1. Check if the position is visible for player
            if (IsVisibleToCamera(spawnPosition))
                return GetSpawnPosition();
            
            // 2. Check if the position is too close to other colliders
            var nearestColliders = new Collider2D[15];
            var radius = _settings.MinDistanceFromOtherCollidersWhenSpawn;
            if (Physics2D.OverlapCircleNonAlloc(spawnPosition, radius, nearestColliders) > 0)
                return GetSpawnPosition();

            return spawnPosition;
        }
        
        private bool IsVisibleToCamera(Vector2 position)
        {
            var cam = Camera.main;
            if (cam == null) return false; // If no camera — no need to block spawn
        
            Vector3 viewportPoint = cam.WorldToViewportPoint(position);

            // z must be positive (in front of camera)
            if (viewportPoint.z < 0)
                return false;

            // Inside camera viewport?
            return viewportPoint.x is >= 0f and <= 1f &&
                   viewportPoint.y is >= 0f and <= 1f;
        }
    }
}