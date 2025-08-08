using Models.Items.Base.Enums;
using UnityEngine;

namespace Models.Items.Base.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SelectableItem", menuName = "ScriptableObjects/SelectableItem")]
    public class SelectableItemSO : ScriptableObject
    {
        public ItemRarity itemRarity;
        public ItemType itemType;
        public string itemName;
        [TextArea]
        public string description;
        public Sprite icon;
        public GameObject prefabToSpawn;
    }
}