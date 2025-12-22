using System;

namespace Models.Modifiers.WeaponsModifierImpl.DataContainers
{
    [Serializable]
    public class WeaponsModifierStats
    {
        public WeaponModifyingValues PlayerShortRangeWeaponModifyingValues;
        public WeaponModifyingValues PlayerMediumRangeWeaponModifyingValues;
        public WeaponModifyingValues PlayerLongRangeWeaponModifyingValues;
        public WeaponModifyingValues DefaultEnemyWeaponModifyingValues;
        public WeaponModifyingValues BossWeaponModifyingValues;
    }
}