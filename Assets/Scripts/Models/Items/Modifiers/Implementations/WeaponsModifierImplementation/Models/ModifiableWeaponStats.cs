using System;

namespace Models.Items.Modifiers.Implementations.WeaponsModifierImplementation.Models
{
    [Serializable]
    public class ModifiableWeaponStats
    {
        public float ReloadTimeModifier = 1f;
        public float SpreadModifier = 1f;
        public float DamageModifier = 1f;
        public float SpeedModifier = 1f;
        public float CooldownTimeModifier = 1f;
    }
}