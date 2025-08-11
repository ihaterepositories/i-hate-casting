using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UserInterface.Animators.Enums;

namespace UserInterface.Animators
{
    public class ScreenBorderAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("appearanceDuration")] [SerializeField] private float _appearanceDuration = 0.5f;
        [FormerlySerializedAs("hideDuration")] [SerializeField] private float _hideDuration = 0.5f;
        [FormerlySerializedAs("screenBorderObject")] [SerializeField] private Image _screenBorderObject;
        [FormerlySerializedAs("itemSelectMenuBorder")] [SerializeField] private Sprite _itemSelectMenuBorder;
        [FormerlySerializedAs("castingMenuBorder")] [SerializeField] private Sprite _castingMenuBorder;

        private void OnDisable()
        {
            _screenBorderObject.DOKill();
        }
        
        public void HideBorder()
        {
            _screenBorderObject.rectTransform.DOScale(2f, _hideDuration);
        }

        public void ShowBorder(ScreenBorderType type)
        {
            switch (type)
            {
                case ScreenBorderType.ItemSelectMenuBorder:
                    ShowBorder(_itemSelectMenuBorder);
                    break;
                case ScreenBorderType.CastingMenuBorder:
                    ShowBorder(_castingMenuBorder);
                    break;
                default:
                    throw new UnexpectedEnumValueException<ScreenBorderType>(type);
            }
        }

        private void ShowBorder(Sprite newBorder)
        {
            _screenBorderObject.sprite = newBorder;
            _screenBorderObject.rectTransform.DOScale(1f, _appearanceDuration);
        }
    }
}