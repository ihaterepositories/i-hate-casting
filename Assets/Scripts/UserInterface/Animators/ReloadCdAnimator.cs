using System.Collections;
using Core.ItemSpawners;
using DG.Tweening;
using Models.Items.Weapons.Implementations.PlayerWeaponImplementation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UserInterface.Animators
{
    public class ReloadCdAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("cdIcon")] [SerializeField] private Image _cdIcon;
        [FormerlySerializedAs("defaultCdSprite")] [SerializeField] private Sprite _defaultCdSprite;
        [FormerlySerializedAs("brokenCdSprite")] [SerializeField] private Sprite _brokenCdSprite;
        
        [FormerlySerializedAs("defaultWeaponSpawner")]
        [Header("Needed to get spawned PlayerWeapon component")]
        [SerializeField] private DefaultWeaponSpawner _defaultWeaponSpawner;
        
        private PlayerWeapon _playerWeapon;

        private void OnEnable()
        {
            _defaultWeaponSpawner.OnSpawned += AssignPlayerWeapon;
        }
    
        private void OnDisable()
        {
            _defaultWeaponSpawner.OnSpawned -= AssignPlayerWeapon;
            _playerWeapon.OnReloadNeeded -= AnimateEmptyMagazine;
            _playerWeapon.OnReloadStarted -= AnimateReloading;

            _cdIcon.DOKill();
        }
        
        private void AssignPlayerWeapon(GameObject weapon)
        {
            _playerWeapon = weapon.GetComponent<PlayerWeapon>();
            
            _playerWeapon.OnReloadNeeded += AnimateEmptyMagazine;
            _playerWeapon.OnReloadStarted += AnimateReloading;
        }

        private void AnimateEmptyMagazine()
        {
            _cdIcon.sprite = _brokenCdSprite;
            _cdIcon.rectTransform.DOLocalMove(new Vector3(-100, 400), 1f).SetEase(Ease.OutBounce);
        }
        
        private void AnimateReloading(float reloadTime)
        {
            StartCoroutine(AnimateReloadingCoroutine(reloadTime));
        }

        private IEnumerator AnimateReloadingCoroutine(float reloadTime)
        {
            _cdIcon.rectTransform.DOLocalMove(new Vector3(300, 400), 0.25f);
        
            yield return new WaitForSeconds(reloadTime);
            
            // To prevent the animation brake with small reload time
            _cdIcon.DOKill();
            _cdIcon.rectTransform.localPosition = new Vector3(300, 400);
            //
        
            _cdIcon.sprite = _defaultCdSprite;
            _cdIcon.rectTransform.DOLocalMove(new Vector3(-100, 400), 0.5f).SetEase(Ease.OutBounce)
                .OnComplete(() => _cdIcon.rectTransform.DOLocalMove(new Vector3(-100, 0), 0.25f));
        }
    }
}
