using Models.Items.Bullets.Base.Enums;

namespace Models.Items.Weapons.Base.Shooting.Interfaces
{
    /// <summary>
    /// Creates a bullet and handles the reloading and cooldown.
    /// </summary>
    public interface IShootService
    {
        public float CooldownDuration { get; }
        public float CooldownTimeElapsed { get; }
        
        /// <summary>
        /// Invoke this method in the Update
        /// to enable shooting when the shoot conditions are met.
        /// </summary>
        public void EnableShoot();
        
        /// <summary>
        /// Use this method to change the type of bullets the weapon shoots.
        /// </summary>
        /// <param name="bulletType"></param>
        public void ChangeBulletsType(BulletType bulletType);
    }
}