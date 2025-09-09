using Models.Creatures.Base.Enums;
using UnityEngine;

namespace Models.Creatures.Base.StatsHandling.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CreatureStats", menuName = "ScriptableObjects/CreatureStats")]
    public class CreatureStatsSo : ScriptableObject
    {
        public CreatureType CreatureType;
        public float MaxHealth;
        public float Speed;
        public float BurstDuration = 0.2f;
        public float WhileBurstSpeedIncreaseCoefficient = 3f;
        public float BurstCooldown = 5f;
    }
}