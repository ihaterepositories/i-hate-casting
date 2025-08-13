using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Animators.Enums;

namespace UserInterface.Animators
{
    public class ScreenBorderAnimator : MonoBehaviour
    {
        [SerializeField] private float _appearanceDuration = 0.5f;
        [SerializeField] private float _hideDuration = 0.5f;
        [SerializeField] private Image _screenBorderObject;
        [SerializeField] private Sprite _itemSelectMenuBorder;
        [SerializeField] private Sprite _castingMenuBorder;

        private void OnDisable()
        {
            _screenBorderObject.DOKill();
        }
        
        public void HideBorder()
        {
            _screenBorderObject.rectTransform.DOScale(2f, _hideDuration)
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
                default:
                    throw new UnexpectedEnumValueException<ScreenBorderType>(type);
            }
        }

        private void ChangeAndMoveBorderToScreen(Sprite newBorder)
        {
            _screenBorderObject.sprite = newBorder;
            _screenBorderObject.rectTransform.DOScale(1f, _appearanceDuration)
                .SetUpdate(true);
        }
    }
}