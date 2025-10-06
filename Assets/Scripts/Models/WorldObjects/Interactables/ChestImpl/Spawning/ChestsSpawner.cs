using System.Collections;
using Models.WorldObjects.Base.Spawning;
using UnityEngine;

namespace Models.WorldObjects.Interactables.ChestImpl.Spawning
{
    public class ChestsSpawner : WorldObjectsSpawner<Chest>
    {
        [Header("Settings")] 
        [SerializeField] private float _spawnInterval;
        
        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }
        
        // TODO: Implement spawn stop when game over and maybe smth else
        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);
                Spawn();
            }
        }
    }
}