using System;

namespace Models.Modifiers.WeaponsModifierImpl.DataContainers
{
    [Serializable]
    public class WeaponsModifierStats
    {
        public WeaponModifyingValues PlayerWeapon;
        public WeaponModifyingValues DefaultEnemyWeaponModifyingValues;
        public WeaponModifyingValues BossWeaponModifyingValues;
    }
}