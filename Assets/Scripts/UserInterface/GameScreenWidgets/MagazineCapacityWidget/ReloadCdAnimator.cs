using System.Collections;
using DG.Tweening;
using Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation;
using Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation.Spawners;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.GameScreenWidgets.MagazineCapacityWidget
{
    public class ReloadCdAnimator : MonoBehaviour
    {
        [SerializeField] private Image _cdIcon;
        [SerializeField] private Sprite _defaultCdSprite;
        [SerializeField] private Sprite _brokenCdSprite;
        
        [Header("Needed to get spawned PlayerWeapon component")]
        [SerializeField] private MainWeaponSpawner _mainWeaponSpawner;
        
        private PlayerWeapon _playerWeapon;
        private Vector3 _defaultPosition;

        private void Awake()
        {
            _defaultPosition = _cdIcon.rectTransform.localPosition;
        }

        private void OnEnable()
        {
            _mainWeaponSpawner.OnItemSpawned += AssignPlayerWeapon;
        }
    
        private void OnDisable()
        {
            _mainWeaponSpawner.OnItemSpawned -= AssignPlayerWeapon;
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
            _cdIcon.rectTransform.DOLocalMove(new Vector3(_defaultPosition.x, _defaultPosition.y+350f), 1f).SetEase(Ease.OutBounce);
        }
        
        private void AnimateReloading(float reloadTime)
        {
            StartCoroutine(AnimateReloadingCoroutine(reloadTime));
        }

        private IEnumerator AnimateReloadingCoroutine(float reloadTime)
        {
            _cdIcon.rectTransform.DOLocalMove(new Vector3(_defaultPosition.x+400f, _defaultPosition.y+350f), 0.25f);
        
            yield return new WaitForSeconds(reloadTime);
            
            // To prevent the animation brake with small reload time
            _cdIcon.DOKill();
            _cdIcon.rectTransform.localPosition = new Vector3(_defaultPosition.x+400f, _defaultPosition.y+350f);
            //
        
            _cdIcon.sprite = _defaultCdSprite;
            _cdIcon.rectTransform.DOLocalMove(new Vector3(_defaultPosition.x, _defaultPosition.y+350f), 0.5f).SetEase(Ease.OutBounce)
                .OnComplete(() => _cdIcon.rectTransform.DOLocalMove(_defaultPosition, 0.25f));
        }
    }
}
