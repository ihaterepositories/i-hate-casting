using System;
using System.Threading.Tasks;
using Core.TimerEngines.Interfaces;
using Models.Bullets;
using Models.Weapons.Dtos;
using Models.Weapons.Services.Reloaders.Interfaces;
using Models.Weapons.Services.StatsScalers.Interfaces;
using Spawners.Interfaces;

namespace Models.Weapons.Services.Reloaders.Base
{
    public abstract class WeaponReloader : IWeaponReloader
    {
        private readonly WeaponStats _stats;
        private readonly IWeaponStatsScaler _statsScaler;
        private readonly ISpawner<Bullet> _bulletsSpawner;
        public readonly ITimerEngine _timerEngine;
        
        private int _currentBulletsCount;
        
        private bool _isMagazineEmpty;
        private bool _isReloading;

        protected WeaponReloader(
            WeaponStats stats,
            IWeaponStatsScaler statsScaler,
            ISpawner<Bullet> bulletsSpawner,
            ITimerEngine timerEngine)
        {
            _stats = stats;
            _statsScaler = statsScaler;
            _bulletsSpawner = bulletsSpawner;
            _timerEngine = timerEngine;

            _currentBulletsCount = _stats.MagazineCapacity;
        }
        
        public int CurrentBulletsCount => _currentBulletsCount;
        public int MagazineCapacity => _stats.MagazineCapacity;
        public bool IsMagazineFull => _currentBulletsCount == _stats.MagazineCapacity;
        public bool IsMagazineEmpty => _isMagazineEmpty;
        
        public event Action<float, float> OnCurrentBulletsCountChanged;
        public event Action OnMagazineEmptied;
        public event Action OnReloaded;
        
        public void Tick()
        {
            if (!IsReloadRuleComplied()) return;
            
            if (!_isMagazineEmpty || _isReloading) return;
            
            StartReload();
        }
        
        private void StartReload()
        {
            _isReloading = true;
            _timerEngine.StartTimer(_statsScaler.ScaleReloadTime(_stats.ReloadTime), Reload);
        }

        private void Reload()
        {
            _currentBulletsCount = _stats.MagazineCapacity;
            _isMagazineEmpty = false;
            _isReloading = false;
            OnReloaded?.Invoke();
            OnCurrentBulletsCountChanged?.Invoke(_currentBulletsCount, MagazineCapacity);
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

            var bullet = _bulletsSpawner.Spawn();
            
            OnCurrentBulletsCountChanged?.Invoke(_currentBulletsCount, MagazineCapacity);
            
            return bullet;
        }
        
        protected abstract bool IsReloadRuleComplied();
    }
}