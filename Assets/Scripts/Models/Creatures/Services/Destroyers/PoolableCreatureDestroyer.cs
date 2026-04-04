using System;
using Models.Creatures.Services.Destroyers.Interfaces;

namespace Models.Creatures.Services.Destroyers
{
    public class PoolableCreatureDestroyer : ICreatureDestroyer
    {
        private readonly Creature _creature;

        public PoolableCreatureDestroyer(Creature creature)
        {
            _creature = creature;
        }

        public event Action OnDestroyed;

        public void DestroyCreature()
        {
            OnDestroyed?.Invoke();
            _creature.ReturnToPool();
        }
    }
}