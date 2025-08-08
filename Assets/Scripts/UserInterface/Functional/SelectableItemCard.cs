using Models.Items.Base.Enums;
using Models.Items.Base.ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class SelectableItemCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemDescriptionText;
        [SerializeField] private Image itemImage;
        [SerializeField] private Button button;
        [SerializeField] private Sprite commonCardSprite;
        [SerializeField] private Sprite goldCardSprite;
        [SerializeField] private Sprite incredibleCardSprite;

        public void SetData(SelectableItemSO itemData)
        {
            itemTypeText.text = itemData.itemType.ToString();
            itemNameText.text = itemData.itemName;
            itemDescriptionText.text = itemData.description;
            itemImage.sprite = itemData.icon;
        }

        private void SetCardSprite(ItemRarity rarity)
        {
            switch (rarity)
            {
                case ItemRarity.Common: button.image.sprite = commonCardSprite; break;
                case ItemRarity.Gold: button.image.sprite = goldCardSprite; break;
                case ItemRarity.Incredible: button.image.sprite = incredibleCardSprite; break;
                default: throw new UnexpectedEnumValueException<ItemRarity>(rarity);
            }
        }
        
        public void AddOnClickAction(System.Action onClickAction)
        {
            button.onClick.AddListener(() => onClickAction?.Invoke());
        }
    }
}