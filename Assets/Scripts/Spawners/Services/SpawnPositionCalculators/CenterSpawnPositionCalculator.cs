using Spawners.Services.SpawnPositionCalculators.Interfaces;
using UnityEngine;

namespace Spawners.Services.SpawnPositionCalculators
{
    public class CenterSpawnPositionCalculator : ISpawnPositionCalculator
    {
        public Vector3 GetSpawnPosition()
        {
            return Vector3.zero;
        }
    }
}