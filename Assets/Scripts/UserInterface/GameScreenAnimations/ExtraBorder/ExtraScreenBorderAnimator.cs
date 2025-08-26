using Core;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.GameScreenAnimations.ExtraBorder.Enums;

namespace UserInterface.GameScreenAnimations.ExtraBorder
{
    public class ExtraScreenBorderAnimator : MonoBehaviour
    {
        [SerializeField] private Image _screenBorderObject;
        [SerializeField] private Sprite _itemSelectMenuBorder;
        [SerializeField] private Sprite _castingMenuBorder;
        
        private Vector3 _defaultScale;

        private void Awake()
        {
            _defaultScale = _screenBorderObject.rectTransform.localScale;
        }

        private void OnDisable()
        {
            _screenBorderObject.DOKill();
        }
        
        public void HideBorder()
        {
            _screenBorderObject.rectTransform.DOScale(_defaultScale, AppConstants.ExtraScreenBorderAppearanceTime)
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
            _screenBorderObject.sprite = newBorder;
            _screenBorderObject.rectTransform.DOScale(1f, AppConstants.ExtraScreenBorderAppearanceTime)
                .SetUpdate(true);
        }
    }
}