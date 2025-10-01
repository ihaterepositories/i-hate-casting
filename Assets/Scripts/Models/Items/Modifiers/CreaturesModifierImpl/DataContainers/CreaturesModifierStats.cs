using System;

namespace Models.Items.Modifiers.CreaturesModifierImpl.DataContainers
{
    [Serializable]
    public class CreaturesModifierStats
    {
        public CreatureModifyingValues PlayerModifyingValues;
        public CreatureModifyingValues DefaultEnemiesModifyingValues;
        public CreatureModifyingValues BossModifyingValues;
    }
}