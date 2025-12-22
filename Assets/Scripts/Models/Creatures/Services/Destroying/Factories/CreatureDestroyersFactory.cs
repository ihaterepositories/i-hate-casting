using System;
using Models.Creatures.Services.Destroying.Enums;
using Models.Creatures.Services.Destroying.Interfaces;

namespace Models.Creatures.Services.Destroying.Factories
{
    public class CreatureDestroyersFactory
    {
        public ICreatureDestroyer Create(CreatureDestroyType destroyType, Creature instance)
        {
            return destroyType switch
            {
                CreatureDestroyType.Poolable => new PoolableCreatureDestroyer(instance),
                CreatureDestroyType.OnlyNotify => new OnlyNotifyCreatureDestroyer(instance),
                _ => throw new ArgumentOutOfRangeException(nameof(destroyType), destroyType, null)
            };
        }
    }
}