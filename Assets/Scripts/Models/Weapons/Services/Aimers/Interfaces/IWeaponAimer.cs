using UnityEngine;

namespace Models.Weapons.Services.Aimers.Interfaces
{
    /// <summary>
    /// Calculates the rotation required to aim a weapon towards a target.
    /// </summary>
    public interface IWeaponAimer
    {
        /// <summary>
        /// Performs one update step and returns the desired weapon rotation.
        /// Should be called every frame (e.g., from Update).
        /// </summary>
        /// <returns>
        /// A <see cref="Quaternion"/> representing the rotation that should be applied to the weapon.
        /// </returns>
        public Quaternion Tick();
    }
}