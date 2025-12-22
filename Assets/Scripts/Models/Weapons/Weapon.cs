using Core.Pausing.Interfaces;
using Models.Weapons.Dtos;
using Models.Weapons.Enums;
using Models.Weapons.Services.Aiming.Enums;
using Models.Weapons.Services.Aiming.Factories;
using Models.Weapons.Services.Aiming.Interfaces;
using Models.Weapons.Services.Reloading.Enums;
using Models.Weapons.Services.Reloading.Factories;
using Models.Weapons.Services.Reloading.Interfaces;
using Models.Weapons.Services.Shooting.Enums;
using Models.Weapons.Services.Shooting.Factories;
using Models.Weapons.Services.Shooting.Interfaces;
using Models.Weapons.Services.StatsCalculating.Factories;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using UnityEngine;
using Zenject;

namespace Models.Weapons
{
    /// <summary>
    /// Helper class with a common functionality for all weapons.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private BulletConfigKey _bulletToUse;
        [SerializeField] private ShootType _shootType;
        [SerializeField] private ReloadType _reloadType;
        [SerializeField] private AimType _aimType;
        [SerializeField] private WeaponStats _weaponStats;

        private IWeaponStatsCalculator _weaponStatsCalculator;
        private IShootService _shooter;
        private IMagazineService _magazine;
        private IAimService _aimer;
        private IPauser _pauser;
        
        public IShootService Shooter => _shooter;
        public IMagazineService Magazine => _magazine;

        [Inject]
        private void Construct(
            WeaponStatsCalculatorsFactory statsCalculatorsFactory,
            WeaponMagazinesFactory weaponMagazinesFactory,
            WeaponShootersFactory weaponShootersFactory,
            WeaponAimersFactory weaponAimersFactory,
            IPauser pauser)
        {
            _weaponStatsCalculator = statsCalculatorsFactory.Create(_weaponType, _weaponStats);
            
            _magazine = weaponMagazinesFactory.Create(
                _bulletToUse,
                _reloadType,
                _weaponStatsCalculator);

            _shooter = weaponShootersFactory.Create(
                _shootType,
                _weaponStatsCalculator,
                _magazine,
                this.transform);

            _aimer = weaponAimersFactory.Create(
                _aimType,
                _weaponStatsCalculator,
                this.transform);
            
            _pauser = pauser;
        }
        
        private void Update()
        {
            if (_pauser.IsGamePaused) return;
            
            _aimer.UpdateAiming();
            _shooter.EnableShoot();
            _magazine.EnableReload();
        }
    }
}