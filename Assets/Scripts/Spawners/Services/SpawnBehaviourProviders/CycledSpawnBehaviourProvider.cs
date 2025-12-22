using System;
using System.Collections;
using Spawners.Services.SpawnBehaviourProviders.Interfaces;
using UnityEngine;

namespace Spawners.Services.SpawnBehaviourProviders
{
    public class CycledSpawnBehaviourProvider : ISpawnBehaviourExecutor
    {
        public IEnumerator Launch(Action spawn, float delayBeforeNextSpawn)
        {
            yield return new WaitForSeconds(delayBeforeNextSpawn);
            spawn();
            yield return Launch(spawn, delayBeforeNextSpawn);
        }
    }
}