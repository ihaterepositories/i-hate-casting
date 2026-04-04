namespace Shared.Services.SpriteFlippers.Interfaces
{
    /// <summary>
    /// Faces sprite to its moving direction.
    /// </summary>
    public interface ISpriteFlipper
    {
        /// <summary>
        /// Performs one update step, monitoring object`s movement and flipping sprite if needed.
        /// Should be called every frame (e.g., from Update).
        /// </summary>
        public void Tick();
    }
}