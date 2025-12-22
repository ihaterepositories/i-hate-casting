using System;
using UnityEngine;

namespace Models.Creatures.Dtos
{
    [Serializable]
    public class CreatureStats
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _speed;
        [SerializeField] private float _boostStrength;
        [SerializeField] private float _boostDuration;
        [SerializeField] private float _boostCooldownTime;
        
        public float MaxHealth => _maxHealth;
        public float Speed  => _speed;
        public float BoostStrength => _boostStrength;
        public float BoostDuration => _boostDuration;
        public float BoostCooldownTime => _boostCooldownTime;
    }
}