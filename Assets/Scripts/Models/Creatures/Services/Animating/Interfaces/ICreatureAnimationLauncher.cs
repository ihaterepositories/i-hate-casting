namespace Models.Creatures.Services.Animating.Interfaces
{
    public interface ICreatureAnimationLauncher
    {
        /// <summary>
        /// Call it immediately after animator has been created.
        /// Subscribes trigger based animations on events.
        /// </summary>
        public void InitializeTriggerAnimations();
        
        /// <summary>
        /// Call it in the FixedUpdate loop.
        /// </summary>
        public void AnimateMoving();

        /// <summary>
        /// Call it in the OnDisable to clean resources.
        /// </summary>
        public void Dispose();
    }
}