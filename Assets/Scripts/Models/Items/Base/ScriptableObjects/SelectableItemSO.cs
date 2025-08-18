using Models.Items.Base.Enums;
using UnityEngine;

namespace Models.Items.Base.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SelectableItem", menuName = "ScriptableObjects/SelectableItem")]
    public class SelectableItemSo : ScriptableObject
    {
        public ItemRarity ItemRarity;
        public ItemType ItemType;
        public string ItemName;
        [TextArea] public string Description;
        public Sprite Icon;
        public GameObject PrefabToSpawn;
    }
}