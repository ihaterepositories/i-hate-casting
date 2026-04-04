using Shared.Systems.ResourcesCleaning.Interfaces;

namespace Models.Creatures.Services.Animators.Interfaces
{
    /// <summary>
    /// Plays creature`s animations due to its state.
    /// (Based on Unity's Animator)
    /// </summary>
    public interface ICreatureAnimator : IResourceCleanable
    {
        /// <summary>
        /// Synchronizes animation state with continuous gameplay state (e.g., movement).
        /// Should be called every frame (e.g., from Update).
        /// </summary>
        public void Tick();
    }
}