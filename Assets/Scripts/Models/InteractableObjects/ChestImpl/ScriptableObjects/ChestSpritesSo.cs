using UnityEngine;

namespace Models.InteractableObjects.ChestImpl.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ChestSprites", menuName = "ScriptableObjects/DataContainers/ChestSprites")]
    public class ChestSpritesSo : ScriptableObject
    {
        public Sprite DefaultChestSprite;
        public Sprite OpenedChestSprite;
    }
}