using System.Collections.Generic;
using Models.WorldObjects.Base.Spawning.Constants;
using Models.WorldObjects.Creatures.PlayerImpl;
using UnityEngine;

namespace Models.WorldObjects.Base.Spawning.Utils
{
    public class SafeSpawnProvider
    {
        private static List<Vector2> _usedPositions = new();
        private bool _checkUsedPositions;
        private Player _player;

        public SafeSpawnProvider(Player player, bool checkUsedPositions)
        {
            _player = player;
            _checkUsedPositions = checkUsedPositions;
        }
        
        public Vector2 GetSafeSpawnPosition()
        {
            // Generate a random position within the spawn radius\
            var spawnRadius = SpawnConstants.SpawnAreaSize;
            var spawnPosition = new Vector2(Random.Range(-spawnRadius.x, spawnRadius.x),
                Random.Range(-spawnRadius.y, spawnRadius.y));
            
            // Check if the position is too close to the player
            var safeDistanceFromPlayer = SpawnConstants.MinDistanceFromPlayerWhenSpawn;
            if (Vector2.Distance(spawnPosition, _player.transform.position) < safeDistanceFromPlayer)
                return GetSafeSpawnPosition();

            // Check if the position has been used recently (if enabled)
            if (_checkUsedPositions)
            {
                if (IsPositionUsed(spawnPosition))
                    return GetSafeSpawnPosition(); 

                _usedPositions.Add(spawnPosition);
            }
            
            // Check if the position is too close to other colliders
            var nearestColliders = new Collider2D[15];
            var radius = SpawnConstants.MinDistanceFromOtherCollidersWhenSpawn;
            if (Physics2D.OverlapCircleNonAlloc(spawnPosition, radius, nearestColliders) > 0)
                return GetSafeSpawnPosition();

            return spawnPosition;
        }
        
        private bool IsPositionUsed(Vector2 position)
        {
            foreach (var usedPosition in _usedPositions)
            {
                if (Vector2.Distance(usedPosition, position) < 0.1f)
                {
                    return true;
                }
            }
            return false;
        }
    }
}