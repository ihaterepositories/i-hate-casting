using System.Collections;
using System.Globalization;
using DG.Tweening;
using Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation;
using Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation.Spawners;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.GameScreenWidgets.MagazineCapacityWidget
{
    public class MagazineCapacityText : MonoBehaviour
    {
        [SerializeField] private Text _magazineCapacityText;
        
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
            StartCoroutine(ShowReloadCountdownCoroutine(time));
        }

        private IEnumerator ShowReloadCountdownCoroutine(float time)
        {
            _magazineCapacityText.text = time.ToString("F1", CultureInfo.InvariantCulture) + "s";
            
            yield return new WaitForSeconds(0.1f);
            time -= 0.1f;
            
            if (time > 0)
            {
                StartCoroutine(ShowReloadCountdownCoroutine(time));
            }
            else
            {
                _isReloading = false;
                _magazineCapacityText.DOColor(_defaultTextColor, 0.5f).SetEase(Ease.OutBounce);
                UpdateText();
            }
        }
    }
}