using System;
using Models.Bullets;

namespace Models.Weapons.Services.Reloaders.Interfaces
{
    /// <summary>
    /// Manages weapon ammunition, including bullet supply, reloading, and state tracking.
    /// </summary>
    public interface IWeaponReloader
    {
        /// <summary>
        /// Current number of bullets available in the magazine.
        /// </summary>
        public int CurrentBulletsCount { get; }
    
        /// <summary>
        /// Maximum number of bullets the magazine can hold.
        /// </summary>
        public int MagazineCapacity { get; }
    
        /// <summary>
        /// Indicates whether the magazine is full.
        /// </summary>
        public bool IsMagazineFull { get; }
    
        /// <summary>
        /// Indicates whether the magazine is empty.
        /// </summary>
        public bool IsMagazineEmpty { get; }
    
        /// <summary>
        /// Invoked when the current bullets count changes.
        /// Parameters: (current, max).
        /// </summary>
        public event Action<float, float> OnCurrentBulletsCountChanged;
    
        /// <summary>
        /// Invoked when the magazine becomes empty.
        /// </summary>
        public event Action OnMagazineEmptied;
    
        /// <summary>
        /// Invoked when reloading is completed.
        /// </summary>
        public event Action OnReloaded;
    
        /// <summary>
        /// Performs one update step, triggering reload logic if conditions are met.
        /// Should be called every frame (e.g., from Update).
        /// </summary>
        public void Tick();
    
        /// <summary>
        /// Provides a bullet for shooting and updates the internal bullets count.
        /// </summary>
        /// <returns>
        /// A spawned <see cref="Bullet"/> instance.
        /// </returns>
        public Bullet GetBullet();
    }
}