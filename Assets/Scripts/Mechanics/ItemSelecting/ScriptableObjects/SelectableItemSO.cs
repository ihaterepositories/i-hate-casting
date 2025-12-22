using Mechanics.ItemSelecting.Enums;
using UnityEngine;

namespace Mechanics.ItemSelecting.ScriptableObjects
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