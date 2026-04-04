namespace Models.Weapons.Services.Shooters.Interfaces
{
    /// <summary>
    /// Handles weapon's shooting logic, including cooldown management and firing conditions.
    /// </summary>
    public interface IWeaponShooter
    {
        /// <summary>
        /// Total time required between consecutive shots.
        /// </summary>
        public float CooldownTime { get; }
        
        /// <summary>
        /// Time elapsed since the last shot.
        /// </summary>
        public float CooldownTimeElapsed { get; }
        
        /// <summary>
        /// Performs one update step, progressing cooldown and triggering a shot when conditions are met.
        /// Should be called every frame (e.g., from Update).
        /// </summary>
        public void Tick();
    }
}