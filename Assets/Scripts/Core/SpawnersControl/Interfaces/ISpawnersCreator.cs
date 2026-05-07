using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Creatures;
using Models.Creatures.Enums;
using Spawners.Interfaces;

namespace Core.SpawnersControl.Interfaces
{
    public interface ISpawnersCreator
    {
        public Dictionary<CreatureType, ISpawner<Creature>> CreatureSpawners  { get; }
        public IEnumerator CreateCoroutine();
    }
}