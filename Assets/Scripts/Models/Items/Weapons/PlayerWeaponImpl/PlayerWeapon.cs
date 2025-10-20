using System;
using Core.GameControl;
using Core.Input.Interfaces;
using Models.Items.Bullets.Base.Providers;
using Models.Items.Weapons.Base;
using Models.Items.Weapons.Base.Aiming.Factories;
using Models.Items.Weapons.Base.Reloading.Factories;
using Models.Items.Weapons.Base.Shooting.Factories;
using Models.Items.Weapons.Base.StatsHandling.Providers;
using Zenject;

namespace Models.Items.Weapons.PlayerWeaponImpl
{
    public class PlayerWeapon : Weapon
    {
        [Inject]
        private void Construct(
            WeaponStatsMultipliersProvider statsMultipliersProvider,
            BulletsProvider bulletsProvider,
            MagazinesFactory magazinesFactory,
            ShootersFactory shootersFactory,
            AimersFactory aimersFactory)
        {
            InitializeStatsHandling(statsMultipliersProvider);
            InitializeServices(
                bulletsProvider,
                magazinesFactory,
                shootersFactory,
                aimersFactory);
        }

        private void Update()
        {
            if (GamePauser.IsGamePaused) return;
            
            Aimer.UpdateAiming();
            Shooter.EnableShoot();
            Magazine.EnableReload();
        }
    }
}