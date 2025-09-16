using System;

namespace Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.CreaturesModifierImplementation.ScriptableObjects.SerializingDataContainers
{
    [Serializable]
    public class CreatureModifyingValues
    {
        public float HealthModifier = 1f;
        public float SpeedModifier = 1f;
        public float BurstDurationModifier = 1f;
        public float WhileBurstSpeedMultiplyingModifier = 1f;
        public float BurstCooldown = 1f;
    }
}