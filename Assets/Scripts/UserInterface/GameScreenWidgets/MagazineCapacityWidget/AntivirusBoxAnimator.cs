using System;
using DG.Tweening;
using Models.Creatures.Items.Implementations.Weapons.Base.Enums;
using Models.Creatures.Items.Implementations.Weapons.Implementations.PlayerWeaponImplementation;
using Models.Creatures.Items.Implementations.Weapons.Implementations.PlayerWeaponImplementation.Spawners;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.GameScreenWidgets.MagazineCapacityWidget
{
    public class AntivirusBoxAnimator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image _antivirusBoxImage;
        [SerializeField] private Text _antivirusBoxText;
        [SerializeField] private Image _antivirusBoxCdImage;
        
        [Header("Sprites")]
        [SerializeField] private Sprite _redBoxSprite;
        [SerializeField] private Sprite _greenBoxSprite;
        [SerializeField] private Sprite _blueBoxSprite;
        
        private RectTransform _antivirusBoxRectTransform => _antivirusBoxImage.rectTransform;
        private Vector3 _defaultPosition;

        private void Awake()
        {
            var textColor = _antivirusBoxText.color;
            textColor.a = 0f;
            _antivirusBoxText.color = textColor;
            
            var cdColor = _antivirusBoxCdImage.color;
            cdColor.a = 0f;
            _antivirusBoxCdImage.color = cdColor;
            
            _defaultPosition = _antivirusBoxRectTransform.localPosition;
            _antivirusBoxRectTransform.localPosition = new Vector3(_defaultPosition.x, _defaultPosition.y - 500f);
        }

        private void OnEnable()
        {
            PlayerWeaponSpawner.OnSpawned += ChangeBoxView;
        }

        private void OnDisable()
        {
            PlayerWeaponSpawner.OnSpawned -= ChangeBoxView;
            
            _antivirusBoxImage.DOKill();
        }

        private void ChangeBoxView(PlayerWeapon playerWeapon)
        {
            var textColor = _antivirusBoxText.color;
            var cdColor = _antivirusBoxCdImage.color;
            
            switch (playerWeapon.WeaponType)
            {
                case WeaponType.PlayerShortRange: _antivirusBoxImage.sprite = _redBoxSprite; break;
                case WeaponType.PlayerMediumRange: _antivirusBoxImage.sprite = _blueBoxSprite; break;
                case WeaponType.PlayerLongRange: _antivirusBoxImage.sprite = _greenBoxSprite; break;
                default: throw new ArgumentOutOfRangeException();
            }
            
            _antivirusBoxRectTransform.DOLocalMove(_defaultPosition, 0.5f)
                .SetUpdate(true)
                .SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    textColor.a = 1f;
                    _antivirusBoxText.color = textColor;
                    cdColor.a = 1f;
                    _antivirusBoxCdImage.color = cdColor;
                });
        }
    }
}