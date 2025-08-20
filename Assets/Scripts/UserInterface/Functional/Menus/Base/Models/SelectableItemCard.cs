using System;
using System.Collections;
using DG.Tweening;
using Models.Items.Base.Enums;
using Models.Items.Base.ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UserInterface.Animators;
using UserInterface.Functional.Menus.Base.Models.ScriptableObjects;
using Random = UnityEngine.Random;

namespace UserInterface.Functional.Menus.Base.Models
{
    public class SelectableItemCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemTypeText;
        [SerializeField] private TextMeshProUGUI _itemNameText;
        [SerializeField] private TextMeshProUGUI _itemDescriptionText;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private SelectableItemCardSpritesSo _cardsSprites;
        [SerializeField] private InteractableButtonAnimator _interactableButtonAnimator;

        private Sprite _currentFaceSprite;
        private Sprite _currentBackSprite;
        
        private Vector3 _defaultScale;
        private Vector3 _defaultRotation;

        private void Awake()
        {
            _defaultScale = _rect.localScale;
            _defaultRotation = _rect.localRotation.eulerAngles;
        }

        private void OnDisable()
        {
            _rect.DOKill();
            _button.image.DOKill();
            RemoveOnClickAction();
        }

        public void FillWith(SelectableItemSo itemData)
        {
            RemoveOnClickAction();
            
            _itemTypeText.text = itemData.ItemType.ToString();
            _itemNameText.text = itemData.ItemName;
            _itemDescriptionText.text = itemData.Description;
            _itemImage.sprite = itemData.Icon;
            
            DefineCardSprites(itemData.ItemRarity);
        }
        
        public void AddOnClickAction(System.Action onClickAction)
        {
            _button.onClick.AddListener(() => onClickAction?.Invoke());
        }
        
        public void Refresh()
        {
            ClearAnimationsResults();
            RemoveOnClickAction();
            
            _itemTypeText.text = string.Empty;
            _itemNameText.text = string.Empty;
            _itemDescriptionText.text = string.Empty;
            _itemImage.sprite = _cardsSprites.NoItemIcon;
            
            DefineCardSprites(ItemRarity.Common);
        }

        public void AnimateAppearance()
        {
            StartCoroutine(AnimateAppearanceCoroutine());
        }
        
        private IEnumerator AnimateAppearanceCoroutine()
        {
            _interactableButtonAnimator.enabled = false;
            ClearAnimationsResults();
            ToggleCardElementsVisibility(false);
            _button.image.sprite = _currentBackSprite;
            
            Color c = _button.image.color;
            c.a = 0f;
            _button.image.color = c;

            _button.image.DOFade(1f, 0.5f)
                .SetUpdate(true);
            
            yield return new WaitForSecondsRealtime(1f);
                
            _rect.DOLocalRotate(new Vector3(0f, 90f, 0f), 0.2f)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    ToggleCardElementsVisibility(true);
                    _button.image.sprite = _currentFaceSprite;
                    _rect.DOLocalRotate(Vector3.zero, 0.2f)
                        .SetEase(Ease.OutBounce)
                        .SetUpdate(true)
                        .OnComplete(() =>
                        {
                            _interactableButtonAnimator.enabled = true;
                        });
                });
        }

        private void RemoveOnClickAction()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void DefineCardSprites(ItemRarity rarity)
        {
            switch (rarity)
            {
                case ItemRarity.Common: 
                    _currentFaceSprite = _cardsSprites.CommonCardSprite;
                    _currentBackSprite = _cardsSprites.CommonCardBackSprite;
                    break;
                case ItemRarity.Gold: 
                    _currentFaceSprite = _cardsSprites.GoldCardSprite;
                    _currentBackSprite = _cardsSprites.GoldCardBackSprite;
                    break;
                case ItemRarity.Incredible: 
                    _currentFaceSprite = _cardsSprites.IncredibleCardSprite;
                    _currentBackSprite = _cardsSprites.IncredibleCardBackSprite;
                    break;
                default: throw new UnexpectedEnumValueException<ItemRarity>(rarity);
            }
        }

        private void ToggleCardElementsVisibility(bool isVisible)
        {
            _itemTypeText.gameObject.SetActive(isVisible);
            _itemNameText.gameObject.SetActive(isVisible);
            _itemDescriptionText.gameObject.SetActive(isVisible);
            _itemImage.gameObject.SetActive(isVisible);
        }

        private void ClearAnimationsResults()
        {
            _rect.localScale = _defaultScale;
            _rect.localRotation = Quaternion.Euler(_defaultRotation);
            _rect.DOKill();
            _button.image.DOKill();
        }
    }
}