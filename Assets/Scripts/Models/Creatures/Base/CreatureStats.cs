using System;
using Models.Creatures.Base.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models.Creatures.Base
{
    [Serializable]
    public class CreatureStats
    {
        [FormerlySerializedAs("health")] [SerializeField] private float _health;
        [FormerlySerializedAs("speed")] [SerializeField] private float _speed;

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
        
        public float GetHealth() => _health * _statsMultiplier.GetMultiplier(CreatureStatType.Health);
        public float GetSpeed() => _speed * _statsMultiplier.GetMultiplier(CreatureStatType.Speed);
    }
}