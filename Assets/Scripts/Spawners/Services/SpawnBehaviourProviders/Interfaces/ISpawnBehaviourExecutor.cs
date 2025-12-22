using System;
using System.Collections;

namespace Spawners.Services.SpawnBehaviourProviders.Interfaces
{
    /// <summary>
    /// Provides method to wrap model spawn method with a specific spawn behaviour.
    /// </summary>
    public interface ISpawnBehaviourExecutor
    {
        /// <summary>
        /// Wraps model spawn method with a spawn behaviour. 
        /// </summary>
        /// <param name="spawn">Method that instantiate model.</param>
        /// <param name="delayBeforeNextSpawn">Delay before each instantiate.</param>
        /// <returns></returns>
        public IEnumerator Launch(Action spawn, float delayBeforeNextSpawn);
    }
}