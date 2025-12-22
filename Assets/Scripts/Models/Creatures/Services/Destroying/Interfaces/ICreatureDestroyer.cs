using System;

namespace Models.Creatures.Services.Destroying.Interfaces
{
    public interface ICreatureDestroyer
    {
        public event Action OnDestroyed;
        public void DestroyCreature();
    }
}