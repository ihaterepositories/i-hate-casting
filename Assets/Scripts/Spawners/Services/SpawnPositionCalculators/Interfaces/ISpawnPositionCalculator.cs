using UnityEngine;

namespace Spawners.Services.SpawnPositionCalculators.Interfaces
{
    public interface ISpawnPositionCalculator
    {
        public Vector3 GetSpawnPosition();
    }
}