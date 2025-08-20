using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UserInterface.Functional.Menus.Base.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SelectableItemCardSprites", menuName = "ScriptableObjects/SelectableItemCardSprites")]
    public class SelectableItemCardSpritesSo : ScriptableObject
    {
        public Sprite CommonCardSprite;
        public Sprite CommonCardBackSprite;
        
        public Sprite GoldCardSprite;
        public Sprite GoldCardBackSprite;
        
        public Sprite IncredibleCardSprite;
        public Sprite IncredibleCardBackSprite;
        
        public Sprite NoItemIcon;
    }
}