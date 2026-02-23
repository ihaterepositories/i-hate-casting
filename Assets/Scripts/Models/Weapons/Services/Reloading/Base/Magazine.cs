using System;
using System.Threading.Tasks;
using Models.Bullets;
using Models.Bullets.Dtos;
using Models.Weapons.Services.Reloading.Interfaces;
using Models.Weapons.Services.StatsCalculating.Interfaces;

namespace Models.Weapons.Services.Reloading.Base
{
    public abstract class Magazine : IMagazineService
    {
        private readonly IWeaponStatsCalculator _weaponStatsCalculator;
        
        private int _currentBulletsCount;
        
        private bool _isMagazineEmpty;
        private bool _isReloading;

        protected Magazine(
            IWeaponStatsCalculator weaponStatsCalculator)
        {
            _weaponStatsCalculator = weaponStatsCalculator;
            
            _currentBulletsCount = _weaponStatsCalculator.CalculateMagazineCapacity();
        }
        
        public int CurrentBulletsCount => _currentBulletsCount;
        public int MagazineCapacity => _weaponStatsCalculator.CalculateMagazineCapacity();
        public bool IsMagazineFull => _currentBulletsCount == _weaponStatsCalculator.CalculateMagazineCapacity();
        public bool IsMagazineEmpty => _isMagazineEmpty;
        
        public event Action<float, float> OnCurrentBulletsCountChanged;
        public event Action OnMagazineEmptied;
        public event Action OnReloaded;
        
        public void EnableReload()
        {
            if (!ReloadRule()) return;
            
            if (!_isMagazineEmpty || _isReloading) return;
            
            _ = ReloadAsync();
        }
        
        private async Task ReloadAsync()
        {
            _isReloading = true;
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(_weaponStatsCalculator.CalculateReloadTime()));
                _currentBulletsCount = _weaponStatsCalculator.CalculateMagazineCapacity();
                _isMagazineEmpty = false;
            }
            finally
            {
                _isReloading = false;
                OnReloaded?.Invoke();
                OnCurrentBulletsCountChanged?.Invoke(_currentBulletsCount, MagazineCapacity);
            }
        }

        public Bullet GetBullet()
        {
            if (_isMagazineEmpty) return null;
            
            _currentBulletsCount--;
            if (_currentBulletsCount == 0)
            {
                OnMagazineEmptied?.Invoke();
                _isMagazineEmpty = true;
            }
            
            // var bullet = _bulletsFactory.Create(_bulletConfig);
            
            OnCurrentBulletsCountChanged?.Invoke(_currentBulletsCount, MagazineCapacity);
            
            return bullet;
        }
        
        protected abstract bool ReloadRule();
    }
}