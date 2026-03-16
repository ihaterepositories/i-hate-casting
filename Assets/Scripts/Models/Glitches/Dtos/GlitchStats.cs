using System;
using Models.Creatures.Dtos;
using Models.Weapons.Dtos;
using UnityEngine;

namespace Models.Glitches.Dtos
{
    [Serializable]
    public class GlitchStats
    {
        [Header("Weapon stats modifiers")]
        private WeaponStats _playerWeaponModifiers;
        private WeaponStats _enemyWeaponsModifiers;
        private WeaponStats _bossWeaponsModifiers;
        
        [Header("Creature stats modifiers")]
        private CreatureStats _playerModifiers;
        private CreatureStats _enemiesModifiers;
        private CreatureStats _bossesModifiers;
        
        public WeaponStats PlayerWeaponModifiers => _playerWeaponModifiers;
        public WeaponStats EnemyWeaponsModifiers => _enemyWeaponsModifiers;
        public WeaponStats BossWeaponsModifiers => _bossWeaponsModifiers;
        public CreatureStats PlayerModifiers => _playerModifiers;
        public CreatureStats EnemiesModifiers => _enemiesModifiers;
        public CreatureStats BossesModifiers => _bossesModifiers;
    }
}