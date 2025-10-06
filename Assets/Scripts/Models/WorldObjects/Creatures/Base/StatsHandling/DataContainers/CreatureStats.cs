using System;
using Models.WorldObjects.Creatures.Base.Enums;

namespace Models.WorldObjects.Creatures.Base.StatsHandling.DataContainers
{
    [Serializable]
    public class CreatureStats
    {
        public CreatureType CreatureType;
        public float MaxHealth;
        public float Speed;
        public float BurstDuration = 0.2f;
        public float WhileBurstSpeedIncreaseCoefficient = 3f;
        public float BurstCooldownTime = 5f;
    }
}