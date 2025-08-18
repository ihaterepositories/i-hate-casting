using DG.Tweening;
using Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation;
using Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation.Spawners;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UserInterfaceUtils.Functional
{
    public class MagazineCapacityText : MonoBehaviour
    {
        [SerializeField] private Text _magazineCapacityText;
        
        [FormerlySerializedAs("_baseWeaponSpawner")]
        [FormerlySerializedAs("_defaultWeaponSpawner")]
        [Header("Needed to get spawned PlayerWeapon component")]
        [SerializeField] private MainWeaponSpawner _mainWeaponSpawner;
        
        private PlayerWeapon _playerWeapon;
        private bool _isReloading;

        private readonly Color _defaultTextColor = new Color32(233, 234, 234, 255);
        private readonly Color _warningTextColor = new Color32(229, 109, 109, 255);

        private void OnEnable()
        {
            _mainWeaponSpawner.OnItemSpawned += SetPlayerWeapon;
        }

        private void OnDisable()
        {
            _mainWeaponSpawner.OnItemSpawned -= SetPlayerWeapon;
            _playerWeapon.OnReloadNeeded -= ShowReloadHintText;
            _playerWeapon.OnReloadStarted -= ShowReloadCountdownText;
        }

        private void Update()
        {
            if (_playerWeapon && !_isReloading)
            {
                UpdateText();
            }
        }

        private void SetPlayerWeapon(GameObject weapon)
        {
            _playerWeapon = weapon.GetComponent<PlayerWeapon>();
            
            _playerWeapon.OnReloadNeeded += ShowReloadHintText;
            _playerWeapon.OnReloadStarted += ShowReloadCountdownText;
        }

        private void UpdateText()
        {
            _magazineCapacityText.text = _playerWeapon.BulletsInMagazine + "/" +
                                        _playerWeapon.WeaponStats.GetMagazineCapacity();
        }

        private void ShowReloadHintText()
        {
            _isReloading = true;
            _magazineCapacityText.DOColor(_warningTextColor, 0.5f).SetEase(Ease.OutBounce)
                .OnComplete(() => _magazineCapacityText.text = "press R...");
        }

        private void ShowReloadCountdownText(float time)
        {
            _isReloading = false;
            _magazineCapacityText.DOCounter((int)time, 0, time)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => _magazineCapacityText.DOColor(_defaultTextColor, 0.5f).SetEase(Ease.OutBounce));
        }
    }
}