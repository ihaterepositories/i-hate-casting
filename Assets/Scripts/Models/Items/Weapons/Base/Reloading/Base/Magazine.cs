using System;
using System.Threading.Tasks;
using Models.Items.Bullets.Base;
using Models.Items.Bullets.Base.Enums;
using Models.Items.Bullets.Base.Providers;
using Models.Items.Weapons.Base.Reloading.Interfaces;
using Models.Items.Weapons.Base.StatsHandling;

namespace Models.Items.Weapons.Base.Reloading.Base
{
    public abstract class Magazine : IMagazineService
    {
        private readonly WeaponStatsCalculator _weaponStatsCalculator;
        private readonly BulletsProvider _bulletsProvider;
        
        private int _currentBulletsCount;
        
        private bool _isMagazineEmpty;
        private bool _isReloading;

        protected Magazine(
            WeaponStatsCalculator weaponStatsCalculator,
            BulletsProvider bulletsProvider)
        {
            _weaponStatsCalculator = weaponStatsCalculator;
            _bulletsProvider = bulletsProvider;
            
            _currentBulletsCount = _weaponStatsCalculator.GetMagazineCapacity();
        }
        
        public int CurrentBulletsCount => _currentBulletsCount;
        public int MagazineCapacity => _weaponStatsCalculator.GetMagazineCapacity();
        public bool IsMagazineFull => _currentBulletsCount == _weaponStatsCalculator.GetMagazineCapacity();
        public bool IsMagazineEmpty => _isMagazineEmpty;
        
        public event Action<float, float> OnCurrentBulletsCountChanged;
        public event Action OnMagazineEmptied;
        public event Action OnReloaded;
        
        public void EnableReload()
        {
            if (!ReloadRule()) return;
            
            if (!_isMagazineEmpty || _isReloading) return;
            
            _ = ReloadAsync(_weaponStatsCalculator.GetReloadTime());
        }
        
        private async Task ReloadAsync(float reloadTime)
        {
            _isReloading = true;
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(reloadTime));
                _currentBulletsCount = _weaponStatsCalculator.GetMagazineCapacity();
                _isMagazineEmpty = false;
            }
            finally
            {
                _isReloading = false;
                OnReloaded?.Invoke();
                OnCurrentBulletsCountChanged?.Invoke(_currentBulletsCount, MagazineCapacity);
            }
        }

        public Bullet GetBullet(BulletType bulletType)
        {
            if (_isMagazineEmpty) return null;
            
            _currentBulletsCount--;
            if (_currentBulletsCount == 0)
            {
                OnMagazineEmptied?.Invoke();
                _isMagazineEmpty = true;
            }
            
            var bullet = _bulletsProvider.Create(bulletType);
            bullet.Init(_weaponStatsCalculator);
            
            OnCurrentBulletsCountChanged?.Invoke(_currentBulletsCount, MagazineCapacity);
            
            return bullet;
        }
        
        protected abstract bool ReloadRule();
    }
}