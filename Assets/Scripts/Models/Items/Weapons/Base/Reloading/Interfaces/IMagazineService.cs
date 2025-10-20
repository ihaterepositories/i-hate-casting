using System;
using Models.Items.Bullets.Base;
using Models.Items.Bullets.Base.Enums;

namespace Models.Items.Weapons.Base.Reloading.Interfaces
{
    public interface IMagazineService
    {
        public int CurrentBulletsCount { get; }
        public int MagazineCapacity { get; }
        public bool IsMagazineFull { get; }
        public bool IsMagazineEmpty { get; }
        
        public event Action<float, float> OnCurrentBulletsCountChanged;
        public event Action OnMagazineEmptied;
        public event Action OnReloaded;
        
        /// <summary>
        /// Invoke this method in the Update
        /// to enable reloading when the magazine is empty.
        /// </summary>
        public void EnableReload();
        public Bullet GetBullet(BulletType bulletType);
    }
}