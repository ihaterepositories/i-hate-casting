using System;

namespace Models.Creatures.Services.Destroyers.Interfaces
{
    /// <summary>
    /// Destroys Creature in specified way. Notifies about destroy.
    /// </summary>
    public interface ICreatureDestroyer
    {
        /// <summary>
        /// Invokes when Creature destroyed.
        /// </summary>
        public event Action OnDestroyed;
        
        /// <summary>
        /// Destroys Creature.
        /// </summary>
        public void DestroyCreature();
    }
}