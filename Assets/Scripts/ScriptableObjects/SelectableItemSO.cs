using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SelectableItem", menuName = "ScriptableObjects")]
    public class SelectableItemSO : ScriptableObject
    {
        public string itemName;
        [TextArea]
        public string description;
        public Sprite icon;
        public GameObject prefabToSpawn;
    }
}