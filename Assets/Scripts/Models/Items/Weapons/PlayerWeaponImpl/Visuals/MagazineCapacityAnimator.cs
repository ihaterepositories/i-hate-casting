using System;
using DG.Tweening;
using Models.Items.Weapons.Base.Reloading.Interfaces;
using Models.Items.Weapons.PlayerWeaponImpl.Spawners;
using Models.WorldObjects.Creatures.PlayerImpl;
using TMPro;
using UnityEngine;
using UserInterface.StatusBar;
using Zenject;

namespace Models.Items.Weapons.PlayerWeaponImpl.Visuals
{
    public class MagazineCapacityAnimator : StatusBarAnimator
    {
        [SerializeField] private TextMeshProUGUI _magazineCapacityText;

        private IMagazineService _playerWeaponMagazine;

        private void OnEnable()
        {
            StartPlayerWeaponSpawner.OnSpawned += Initialize;
        }
        
        private void OnDisable()
        {
            StartPlayerWeaponSpawner.OnSpawned -= Initialize;
            
            _playerWeaponMagazine.OnCurrentBulletsCountChanged -= UpdateBarAnimated;
            _playerWeaponMagazine.OnCurrentBulletsCountChanged -= UpdateText;

            Bar.DOKill();
        }

        private void Initialize(PlayerWeapon playerWeapon)
        {
            _playerWeaponMagazine = playerWeapon.Magazine;
            UpdateBar(_playerWeaponMagazine.CurrentBulletsCount, _playerWeaponMagazine.MagazineCapacity);
            
            _playerWeaponMagazine.OnCurrentBulletsCountChanged += UpdateBarAnimated;
            _playerWeaponMagazine.OnCurrentBulletsCountChanged += UpdateText;
            
            UpdateText(_playerWeaponMagazine.CurrentBulletsCount, _playerWeaponMagazine.MagazineCapacity);
        }
        
        private void UpdateText(float currentValue, float maxValue)
        {
            _magazineCapacityText.text = $"{currentValue} / {maxValue}";
        }
    }
}