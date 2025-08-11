using Models.Items.Base.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models.Items.Base.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SelectableItem", menuName = "ScriptableObjects/SelectableItem")]
    public class SelectableItemSo : ScriptableObject
    {
        [FormerlySerializedAs("itemRarity")] public ItemRarity _itemRarity;
        [FormerlySerializedAs("itemType")] public ItemType _itemType;
        [FormerlySerializedAs("itemName")] public string _itemName;
        [FormerlySerializedAs("description")] [TextArea]
        public string _description;
        [FormerlySerializedAs("icon")] public Sprite _icon;
        [FormerlySerializedAs("prefabToSpawn")] public GameObject _prefabToSpawn;
    }
}