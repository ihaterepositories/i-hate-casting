using Core;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.GameScreenAnimations.ExtraBorder.Enums;

namespace UserInterface.GameScreenAnimations.ExtraBorder
{
    public class ScreenBorderAnimator : MonoBehaviour
    {
        [SerializeField] private Image _screenBorder;
        [SerializeField] private Image _extraScreenBorder;
        [SerializeField] private Sprite _itemSelectMenuBorder;
        [SerializeField] private Sprite _castingMenuBorder;

        private Vector3 _borderDefaultScale;
        private Vector3 _extraBorderDefaultScale;

        private void Awake()
        {
            _borderDefaultScale = _screenBorder.rectTransform.localScale;
            _extraBorderDefaultScale = _extraScreenBorder.rectTransform.localScale;
        }

        private void OnDisable()
        {
            _screenBorder.DOKill();
            _extraScreenBorder.DOKill();
        }
        
        public void HideBorder()
        {
            _screenBorder.rectTransform.DOScale(_borderDefaultScale, AppConstants.ExtraScreenBorderAppearanceTime-0.1f)
                .SetUpdate(true);
            _extraScreenBorder.rectTransform.DOScale(_extraBorderDefaultScale, AppConstants.ExtraScreenBorderAppearanceTime)
                .SetUpdate(true);
        }

        public void ShowBorder(ScreenBorderType type)
        {
            switch (type)
            {
                case ScreenBorderType.ItemSelectMenuBorder:
                    ChangeAndMoveBorderToScreen(_itemSelectMenuBorder);
                    break;
                case ScreenBorderType.CastingMenuBorder:
                    ChangeAndMoveBorderToScreen(_castingMenuBorder);
                    break;
                case ScreenBorderType.None:
                    break;
                default:
                    throw new UnexpectedEnumValueException<ScreenBorderType>(type);
            }
        }

        private void ChangeAndMoveBorderToScreen(Sprite newBorder)
        {
            _screenBorder.rectTransform.DOScale(1f, AppConstants.ExtraScreenBorderAppearanceTime-0.1f)
                .SetUpdate(true)
                .SetEase(Ease.OutBounce);
            
            _extraScreenBorder.sprite = newBorder;
            _extraScreenBorder.rectTransform.DOScale(1f, AppConstants.ExtraScreenBorderAppearanceTime)
                .SetUpdate(true);
        }
    }
}