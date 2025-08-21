using UnityEngine;
using UnityEngine.Serialization;

namespace UserInterface.Functional.Menus.Base.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SelectableItemCardTextsColors", menuName = "ScriptableObjects/SelectableItemCardTextsColors")]
    public class SelectableItemCardTextsColorsSo : ScriptableObject
    {
        public Color CommonCardTextColor;
        public Color GoldCardTextColor;
        public Color IncredibleCardTextColor;
    }
}