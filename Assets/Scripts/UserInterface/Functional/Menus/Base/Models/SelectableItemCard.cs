using Models.Items.Base.Enums;
using Models.Items.Base.ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UserInterface.Functional.Menus.Base.Models
{
    public class SelectableItemCard : MonoBehaviour
    {
        [FormerlySerializedAs("itemTypeText")] [SerializeField] private TextMeshProUGUI _itemTypeText;
        [FormerlySerializedAs("itemNameText")] [SerializeField] private TextMeshProUGUI _itemNameText;
        [FormerlySerializedAs("itemDescriptionText")] [SerializeField] private TextMeshProUGUI _itemDescriptionText;
        [FormerlySerializedAs("itemImage")] [SerializeField] private Image _itemImage;
        [FormerlySerializedAs("button")] [SerializeField] private Button _button;
        [FormerlySerializedAs("commonCardSprite")] [SerializeField] private Sprite _commonCardSprite;
        [FormerlySerializedAs("goldCardSprite")] [SerializeField] private Sprite _goldCardSprite;
        [FormerlySerializedAs("incredibleCardSprite")] [SerializeField] private Sprite _incredibleCardSprite;
        [SerializeField] private Sprite _noItemIcon;

        public void AddOnClickAction(System.Action onClickAction)
        {
            _button.onClick.AddListener(() => onClickAction?.Invoke());
        }
        
        public void ClearData()
        {
            _itemTypeText.text = string.Empty;
            _itemNameText.text = string.Empty;
            _itemDescriptionText.text = string.Empty;
            _itemImage.sprite = _noItemIcon;
            
            SetCardSprite(ItemRarity.Common);
        }
        
        public void FillWith(SelectableItemSo itemData)
        {
            RemoveOnClickAction();
            
            _itemTypeText.text = itemData.ItemType.ToString();
            _itemNameText.text = itemData.ItemName;
            _itemDescriptionText.text = itemData.Description;
            _itemImage.sprite = itemData.Icon;
            
            SetCardSprite(itemData.ItemRarity);
        }

        private void RemoveOnClickAction()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void SetCardSprite(ItemRarity rarity)
        {
            switch (rarity)
            {
                case ItemRarity.Common: _button.image.sprite = _commonCardSprite; break;
                case ItemRarity.Gold: _button.image.sprite = _goldCardSprite; break;
                case ItemRarity.Incredible: _button.image.sprite = _incredibleCardSprite; break;
                default: throw new UnexpectedEnumValueException<ItemRarity>(rarity);
            }
        }
    }
}