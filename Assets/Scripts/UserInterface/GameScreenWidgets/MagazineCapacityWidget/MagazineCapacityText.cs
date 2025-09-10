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
        
        private PlayerWeapon _playerWeapon;
        private bool _isReloading;

        private void OnEnable()
        {
            MainPlayerWeaponSpawner.OnSpawned += RegisterPlayerWeapon;
        }

        private void OnDisable()
        {
            MainPlayerWeaponSpawner.OnSpawned -= RegisterPlayerWeapon;
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

        private void RegisterPlayerWeapon(PlayerWeapon weapon)
        {
            _playerWeapon = weapon;
            
            _playerWeapon.OnReloadNeeded += ShowReloadHintText;
            _playerWeapon.OnReloadStarted += ShowReloadCountdownText;
        }

        private void UpdateText()
        {
            _magazineCapacityText.text = _playerWeapon.BulletsInMagazine + "/" +
                                        _playerWeapon.WeaponStatsCalculator.GetMagazineCapacity();
        }

        private void ShowReloadHintText()
        {
            _isReloading = true;
            _magazineCapacityText.DOText("press R...", 0.5f).SetEase(Ease.OutBounce);
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
                UpdateText();
            }
        }
    }
}