using Models.Items.Bullets.Base.Enums;
using Models.Items.Bullets.Base.Providers;
using Models.Items.Weapons.Base.Aiming.Enums;
using Models.Items.Weapons.Base.Aiming.Factories;
using Models.Items.Weapons.Base.Aiming.Interfaces;
using Models.Items.Weapons.Base.Enums;
using Models.Items.Weapons.Base.Reloading.Enums;
using Models.Items.Weapons.Base.Reloading.Factories;
using Models.Items.Weapons.Base.Reloading.Interfaces;
using Models.Items.Weapons.Base.Shooting.Enums;
using Models.Items.Weapons.Base.Shooting.Factories;
using Models.Items.Weapons.Base.Shooting.Interfaces;
using Models.Items.Weapons.Base.StatsHandling;
using Models.Items.Weapons.Base.StatsHandling.DataContainers;
using Models.Items.Weapons.Base.StatsHandling.Providers;
using UnityEngine;

namespace Models.Items.Weapons.Base
{
    /// <summary>
    /// Helper class with a common functionality for all weapons.
    /// </summary>
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private BulletType _bulletType;
        [SerializeField] private ShootType _shootType;
        [SerializeField] private ReloadType _reloadType;
        [SerializeField] private AimType _aimType;
        [SerializeField] private WeaponStats _weaponStats;

        private WeaponStatsCalculator _weaponStatsCalculator;
        
        private IShootService _shooter;
        private IMagazineService _magazine;
        private IAimService _aimer;
        
        protected BulletType BulletType => _bulletType;
        protected IAimService Aimer => _aimer;
        public IShootService Shooter => _shooter;
        public IMagazineService Magazine => _magazine;

        protected void InitializeStatsHandling(WeaponStatsMultipliersProvider statsMultipliersProvider)
        {
            var weaponStatsMultiplier = statsMultipliersProvider.GetFor(_weaponType);
            _weaponStatsCalculator = new WeaponStatsCalculator(_weaponStats, weaponStatsMultiplier);
        }

        protected void InitializeServices(
            BulletsProvider bulletsProvider,
            MagazinesFactory magazinesFactory,
            ShootersFactory shootersFactory,
            AimersFactory aimersFactory)
        {
            _magazine = magazinesFactory.Create(
                _reloadType,
                _weaponStatsCalculator,
                bulletsProvider);

            _shooter = shootersFactory.Create(
                _shootType,
                _weaponStatsCalculator,
                _magazine,
                this.transform);
            
            _shooter.ChangeBulletsType(_bulletType);

            _aimer = aimersFactory.Create(
                _aimType,
                _weaponStatsCalculator,
                this.transform);
        }
    }
}