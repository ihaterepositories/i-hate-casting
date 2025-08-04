using Models.Creatures.Base.Enums;
using UnityEngine;

namespace Models.Creatures.Base
{
    public class CreatureStatsMultiplier : MonoBehaviour
    {
        private float _healthMultiplier = 1f;
        private float _speedMultiplier = 1f;
        
        public float GetMultiplier(CreatureStatType type)
        {
            return type switch
            {
                CreatureStatType.Health => _healthMultiplier,
                CreatureStatType.Speed => _speedMultiplier,
                _ => 1f
            };
        }
        
        /// <summary>
        /// Modify one of creature stats multipliers.
        /// </summary>
        /// <param name="type">Type of creature stat multiplier to modify.</param>
        /// <param name="value">Number that will be added to current multiplier's value.</param>
        public void AddValueToMultiplier(CreatureStatType type, float value)
        {
            switch (type)
            {
                case CreatureStatType.Health: _healthMultiplier += value; break;
                case CreatureStatType.Speed: _speedMultiplier += value; break;
            }
        }
    }
}