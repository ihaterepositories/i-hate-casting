using System;
using Models.Creatures.Base.Enums;
using UnityEngine;

namespace Models.Creatures.Base
{
    [Serializable]
    public class CreatureStats
    {
        [SerializeField] private float health;
        [SerializeField] private float speed;

        private CreatureStatsMultiplier _statsMultiplier;
        
        public void SetStatsMultiplier(CreatureStatsMultiplier statsMultiplier)
        {
            if (statsMultiplier == null)
            {
                Debug.LogError("Creature stats multiplier is null. Cannot assign.");
                return;
            }
            
            _statsMultiplier = statsMultiplier;
        }
        
        public float GetHealth() => health * _statsMultiplier.GetMultiplier(CreatureStatType.Health);
        public float GetSpeed() => speed * _statsMultiplier.GetMultiplier(CreatureStatType.Speed);
    }
}