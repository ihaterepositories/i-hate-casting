using System.Collections.Generic;
using System.Linq;
using Models.Creatures.Implementations.PlayerImplementation;
using Models.InteractableObjects.Base.Pools;
using Models.Pooling;
using UnityEngine;
using Zenject;

namespace Models.InteractableObjects.Base.Spawners
{
    /// <summary>
    /// Parent class for interactable objects spawners, can only spawn objects.
    /// Implement spawn strategy in the child class.
    /// </summary>
    /// <typeparam name="T">Specific interactable object (inherited from InteractableObject class) to spawn.</typeparam>
    public class InteractableObjectsSpawner<T> : MonoBehaviour where T : InteractableObject
    {
        [Header("Dependencies")]
        [SerializeField] private PoolContainer<T> _interactableObjectsPool;
        
        [Header("Settings")]
        [SerializeField] private Vector2 _spawnRadius;
        [SerializeField] private float _minDistanceFromPlayerToSpawn;
        [SerializeField] private float _minDistanceFromOtherInteractableObjectsToSpawn;
        
        private static List<Vector2> _usedPositions = new();
        private Player _player;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        protected void Spawn()
        {
            var interactableObject = _interactableObjectsPool.GetFreeObject();
            interactableObject.transform.position = GetFreePosition();
            interactableObject.OnDestroyed += c => _usedPositions.Remove(c.transform.position);
        }
        
        private Vector2 GetFreePosition()
        {
            var newPosition = new Vector2(Random.Range(-_spawnRadius.x, _spawnRadius.x),
                Random.Range(-_spawnRadius.y, _spawnRadius.y));
            
            if (_usedPositions.Contains(newPosition))
                return GetFreePosition();

            if (Vector2.Distance(newPosition, _player.transform.position) < _minDistanceFromPlayerToSpawn)
                return GetFreePosition();

            if (_usedPositions.Any(up => Vector2.Distance(up, newPosition) < _minDistanceFromOtherInteractableObjectsToSpawn))
                return GetFreePosition();
            
            _usedPositions.Add(newPosition);
            return newPosition;
        }
    }
}