using System;
using Models.Creatures.Services.Destroyers.Interfaces;

namespace Models.Creatures.Services.Destroyers
{
    public class DefaultCreatureDestroyer : ICreatureDestroyer
    {
        private readonly Creature _creature;

        public DefaultCreatureDestroyer(Creature creature)
        {
            _creature = creature;
        }
        
        public event Action OnDestroyed;
        
        public void DestroyCreature()
        {
            OnDestroyed?.Invoke();
        }
    }
}