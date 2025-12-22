using System;
using Models.Creatures.Services.Destroying.Interfaces;

namespace Models.Creatures.Services.Destroying
{
    public class OnlyNotifyCreatureDestroyer : ICreatureDestroyer
    {
        private readonly Creature _creature;

        public OnlyNotifyCreatureDestroyer(Creature creature)
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