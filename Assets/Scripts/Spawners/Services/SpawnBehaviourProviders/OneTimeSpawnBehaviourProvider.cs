using System;
using System.Collections;
using Spawners.Services.SpawnBehaviourProviders.Interfaces;
using UnityEngine;

namespace Spawners.Services.SpawnBehaviourProviders
{
    public class OneTimeSpawnBehaviourProvider : ISpawnBehaviourExecutor
    {
        public IEnumerator Launch(Action spawn, float delayBeforeNextSpawn)
        {
            yield return new WaitForSeconds(delayBeforeNextSpawn);
            spawn();
        }
    }
}