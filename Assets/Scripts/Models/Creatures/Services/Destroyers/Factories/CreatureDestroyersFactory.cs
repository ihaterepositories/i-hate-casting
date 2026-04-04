using System;
using Models.Creatures.Services.Destroyers.Enums;
using Models.Creatures.Services.Destroyers.Interfaces;

namespace Models.Creatures.Services.Destroyers.Factories
{
    public class CreatureDestroyersFactory
    {
        public ICreatureDestroyer Create(CreatureDestroyType destroyType, Creature instance)
        {
            return destroyType switch
            {
                CreatureDestroyType.Poolable => new PoolableCreatureDestroyer(instance),
                CreatureDestroyType.Default => new DefaultCreatureDestroyer(instance),
                _ => throw new ArgumentOutOfRangeException(nameof(destroyType), destroyType, null)
            };
        }
    }
}