using System;

namespace Models.Items.Modifiers.CreaturesModifierImpl.DataContainers
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