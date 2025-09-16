using Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.CreaturesModifierImplementation.ScriptableObjects.SerializingDataContainers;
using UnityEngine;

namespace Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.CreaturesModifierImplementation.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CreaturesModifierStats", menuName = "ScriptableObjects/CreaturesModifierStats")]
    public class CreaturesModifierStatsSo : ScriptableObject
    {
        public CreatureModifyingValues PlayerModifyingValues;
        public CreatureModifyingValues DefaultEnemiesModifyingValues;
        public CreatureModifyingValues BossModifyingValues;
    }
}