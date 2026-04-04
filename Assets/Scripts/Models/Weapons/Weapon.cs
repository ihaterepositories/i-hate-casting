using Core.Pausing.Interfaces;
using Models.Bullets.Enums;
using Models.Weapons.Dtos;
using Models.Weapons.Enums;
using Models.Weapons.Services.Aimers.Enums;
using Models.Weapons.Services.Aimers.Factories;
using Models.Weapons.Services.Aimers.Interfaces;
using Models.Weapons.Services.Reloaders.Enums;
using Models.Weapons.Services.Reloaders.Factories;
using Models.Weapons.Services.Reloaders.Interfaces;
using Models.Weapons.Services.Shooters.Enums;
using Models.Weapons.Services.Shooters.Factories;
using Models.Weapons.Services.Shooters.Interfaces;
using Models.Weapons.Services.StatsScalers.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Models.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [Header("Behaviour settings")]
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private BulletType _bulletType;
        [FormerlySerializedAs("_shootType")] [SerializeField] private WeaponShootType _weaponShootType;
        [FormerlySerializedAs("_reloadType")] [SerializeField] private WeaponReloadType _weaponReloadType;
        [FormerlySerializedAs("_aimType")] [SerializeField] private WeaponAimType _weaponAimType;
        
        [Header("Stats")]
        [SerializeField] private WeaponStats _stats;
        
        private IWeaponShooter _weaponShooter;
        private IWeaponReloader _weaponReloader;
        private IWeaponAimer _weaponAimer;
        private IPauser _pauser;
        
        public IWeaponShooter WeaponShooter => _weaponShooter;
        public IWeaponReloader WeaponReloader => _weaponReloader;

        [Inject]
        private void Construct(
            WeaponStatsScalersProvider statsScalersProvider,
            WeaponReloadersFactory weaponReloadersFactory,
            WeaponShootersFactory weaponShootersFactory,
            WeaponAimersFactory weaponAimersFactory,
            IPauser pauser)
        {
            var statsScaler = statsScalersProvider.GetFor(_weaponType);
            
            _weaponReloader = weaponReloadersFactory.Create(
                _bulletType,
                _weaponReloadType,
                _stats,
                statsScaler);

            _weaponShooter = weaponShootersFactory.Create(
                _weaponShootType,
                _stats,
                statsScaler,
                _weaponReloader,
                transform);

            _weaponAimer = weaponAimersFactory.Create(
                _weaponAimType,
                _stats,
                statsScaler,
                transform);
            
            _pauser = pauser;
        }
        
        private void Update()
        {
            if (_pauser.IsGamePaused) return;
            
            transform.rotation = _weaponAimer.Tick();
            _weaponShooter.Tick();
            _weaponReloader.Tick();
        }
    }
}