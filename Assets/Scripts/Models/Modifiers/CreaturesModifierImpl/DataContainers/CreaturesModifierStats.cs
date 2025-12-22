using System;
using UnityEngine.Serialization;

namespace Models.Modifiers.CreaturesModifierImpl.DataContainers
{
    [Serializable]
    public class CreaturesModifierStats
    {
        [FormerlySerializedAs("PlayerImprovingValues")] public CreatureModifyingValues PlayerModifyingValues;
        [FormerlySerializedAs("DefaultEnemiesImprovingValues")] public CreatureModifyingValues DefaultEnemiesModifyingValues;
    }
}