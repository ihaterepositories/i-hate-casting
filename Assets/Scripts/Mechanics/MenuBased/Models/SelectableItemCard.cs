using System;
using ButtonAnimators;
using DG.Tweening;
using Mechanics.ItemSelecting.Enums;
using Mechanics.ItemSelecting.ScriptableObjects;
using Mechanics.MenuBased.Models.ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics.MenuBased.Models
{
    public class SelectableItemCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemTypeText;
        [SerializeField] private TextMeshProUGUI _itemNameText;
        [SerializeField] private TextMeshProUGUI _itemDescriptionText;
        [SerializeField] private Image _itemImage;
        [SerializeField] private UnityEngine.UI.Button _button;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private InteractableButtonAnimator _interactableButtonAnimator;
        [SerializeField] private SelectableItemCardSpritesSo _cardsSprites;
        [SerializeField] private SelectableItemCardTextsColorsSo _textsColors;

        private Sprite _currentFaceSprite;
        private Sprite _currentBackSprite;
        private Color _currentTextColor;
        
        private Vector3 _defaultScale;
        private Vector3 _defaultRotation;

        private bool _isInitialized;
        
        public bool IsInitialized => _isInitialized;

        private void Awake()
        {
            _defaultScale = _rect.localScale;
            _defaultRotation = _rect.localRotation.eulerAngles;
            _interactableButtonAnimator.enabled = false;
        }

        private void OnDisable()
        {
            _rect.DOKill();
            _button.image.DOKill();
            RemoveOnClickAction();
        }

        /// <summary>
        /// Assigns the item data to the card and sets up the visuals.
        /// Card is flipped to the back side now.
        /// </summary>
        /// <param name="itemData"></param>
        /// <param name="onClickAction">Action that will be called after clicking on a flipped up card.</param>
        public void Initialize(SelectableItemSo itemData, Action onClickAction = null)
        {
            InitializeVisuals(itemData.ItemRarity);
            InitializeItemData(itemData, onClickAction);
            _isInitialized = true;
        }

        /// <summary>
        /// Can`t be called while card is not initialized.
        /// Flips the card up, showing the front side with item data.
        /// </summary>
        /// <param name="onAnimationEndCallback"></param>
        public void AnimateFlipUp(Action onAnimationEndCallback = null)
        {
            if (!_isInitialized) 
            {
                Debug.LogWarning("Card is not initialized to be flipped up.");
                return;
            }
            
            _button.image.DOFade(1f, 0.25f)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _rect.DOLocalRotate(new Vector3(0f, 90f, 0f), 0.1f)
                        .SetUpdate(true)
                        .OnComplete(() =>
                        {
                            ToggleCardElementsVisibility(true);
                            _button.image.sprite = _currentFaceSprite;
                            _rect.DOLocalRotate(Vector3.zero, 0.1f)
                                .SetEase(Ease.OutBounce)
                                .SetUpdate(true)
                                .OnComplete(() =>
                                {
                                    onAnimationEndCallback?.Invoke();
                                    _interactableButtonAnimator.enabled = true;
                                    _isInitialized = true;
                                });
                        });
                });
        }
        
        private void RemoveOnClickAction()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void InitializeVisuals(ItemRarity itemRarity)
        {
            DefineCardVisualsDueToRarity(itemRarity);
            SetCurrentTextColor();
            _button.image.sprite = _currentBackSprite;
            ToggleCardElementsVisibility(false);
            MakeCardInvisible();
            ClearAnimationsResults();
            _interactableButtonAnimator.enabled = false;
        }

        private void InitializeItemData(SelectableItemSo itemData, Action onClickAction = null)
        {
            _button.onClick.AddListener(() => onClickAction?.Invoke());
            _itemTypeText.text = itemData.ItemType.ToString();
            _itemNameText.text = itemData.ItemName;
            _itemDescriptionText.text = itemData.Description;
            _itemImage.sprite = itemData.Icon;
        }
        
        private void DefineCardVisualsDueToRarity(ItemRarity rarity)
        {
            switch (rarity)
            {
                case ItemRarity.Common: 
                    _currentFaceSprite = _cardsSprites.CommonCardSprite;
                    _currentBackSprite = _cardsSprites.CommonCardBackSprite;
                    _currentTextColor = _textsColors.CommonCardTextColor;
                    break;
                case ItemRarity.Gold: 
                    _currentFaceSprite = _cardsSprites.GoldCardSprite;
                    _currentBackSprite = _cardsSprites.GoldCardBackSprite;
                    _currentTextColor = _textsColors.GoldCardTextColor;
                    break;
                case ItemRarity.Incredible: 
                    _currentFaceSprite = _cardsSprites.IncredibleCardSprite;
                    _currentBackSprite = _cardsSprites.IncredibleCardBackSprite;
                    _currentTextColor = _textsColors.IncredibleCardTextColor;
                    break;
                default: throw new UnexpectedEnumValueException<ItemRarity>(rarity);
            }
        }
        
        private void SetCurrentTextColor()
        {
            _itemTypeText.color = _currentTextColor;
            _itemNameText.color = _currentTextColor;
            _itemDescriptionText.color = _currentTextColor;
        }
        
        private void ToggleCardElementsVisibility(bool isVisible)
        {
            ToggleCardTextVisibility(isVisible);
            ToggleCardItemImageVisibility(isVisible);
        }

        private void ToggleCardTextVisibility(bool isVisible)
        {
            float alpha = isVisible ? 1f : 0f;
            Color c = _button.image.color;
            c.a = alpha;
            
            _itemTypeText.color = c;
            _itemNameText.color = c;
            _itemDescriptionText.color = c;
        }

        private void ToggleCardItemImageVisibility(bool isVisible)
        {
            float alpha = isVisible ? 1f : 0f;
            Color c = _itemImage.color;
            c.a = alpha;
            _itemImage.color = c;
        }
        
        private void MakeCardInvisible()
        {
            Color c = _button.image.color;
            c.a = 0f;
            _button.image.color = c;
        }

        private void ClearAnimationsResults()
        {
            _rect.localScale = _defaultScale;
            _rect.localRotation = Quaternion.Euler(_defaultRotation);
        }
    }
}