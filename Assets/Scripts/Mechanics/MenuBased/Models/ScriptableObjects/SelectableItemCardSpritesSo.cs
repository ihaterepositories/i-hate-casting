using UnityEngine;

namespace Mechanics.MenuBased.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SelectableItemCardSprites", menuName = "ScriptableObjects/DataContainers/SelectableItemCardSprites")]
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