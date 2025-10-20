using System;
using Models.Items.Weapons.Base.Shooting.Interfaces;
using Models.Items.Weapons.PlayerWeaponImpl.Spawners;
using UserInterface.StatusBar;

namespace Models.Items.Weapons.PlayerWeaponImpl.Visuals
{
    public class WeaponCooldownAnimator : StatusBarAnimator
    {
        private IShootService _playerWeaponShooter;

        private void OnEnable()
        {
            StartPlayerWeaponSpawner.OnSpawned += Initialize;
        }
        
        private void OnDisable()
        {
            StartPlayerWeaponSpawner.OnSpawned -= Initialize;
        }

        private void Update()
        {
            if (_playerWeaponShooter == null) return;
            
            UpdateBar(_playerWeaponShooter.CooldownTimeElapsed, _playerWeaponShooter.CooldownDuration);
        }

        private void Initialize(PlayerWeapon playerWeapon)
        {
            _playerWeaponShooter = playerWeapon.Shooter;
        }
    }
}