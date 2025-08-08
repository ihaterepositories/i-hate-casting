using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Animators.Enums;

namespace UserInterface.Animators
{
    public class ScreenBorderAnimator : MonoBehaviour
    {
        [SerializeField] private float appearanceDuration = 0.5f;
        [SerializeField] private float hideDuration = 0.5f;
        [SerializeField] private Image screenBorderObject;
        [SerializeField] private Sprite itemSelectMenuBorder;
        [SerializeField] private Sprite castingMenuBorder;

        private void OnDisable()
        {
            screenBorderObject.DOKill();
        }
        
        public void HideBorder()
        {
            screenBorderObject.rectTransform.DOScale(2f, hideDuration);
        }

        public void SetBorder(ScreenBorderType type)
        {
            switch (type)
            {
                case ScreenBorderType.ItemSelectMenuBorder:
                    ShowBorder(itemSelectMenuBorder);
                    break;
                case ScreenBorderType.CastingMenuBorder:
                    ShowBorder(castingMenuBorder);
                    break;
                default:
                    throw new UnexpectedEnumValueException<ScreenBorderType>(type);
            }
        }

        private void ShowBorder(Sprite newBorder)
        {
            screenBorderObject.sprite = newBorder;
            screenBorderObject.rectTransform.DOScale(1f, appearanceDuration);
        }
    }
}