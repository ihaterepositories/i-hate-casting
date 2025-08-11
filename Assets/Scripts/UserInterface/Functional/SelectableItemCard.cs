using Models.Items.Base.Enums;
using Models.Items.Base.ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UserInterface.Functional
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

        public void SetData(SelectableItemSo itemData)
        {
            _itemTypeText.text = itemData._itemType.ToString();
            _itemNameText.text = itemData._itemName;
            _itemDescriptionText.text = itemData._description;
            _itemImage.sprite = itemData._icon;
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
        
        public void AddOnClickAction(System.Action onClickAction)
        {
            _button.onClick.AddListener(() => onClickAction?.Invoke());
        }
    }
}