using System;

namespace Models.Items.Modifiers.Implementations.WeaponsModifierImplementation.ScriptableObjects.SerializingDataContainers
{
    [Serializable]
    public class WeaponModifyingValues
    {
        public float ReloadTimeModifier = 1f;
        public float SpreadModifier = 1f;
        public float DamageModifier = 1f;
        public float SpeedModifier = 1f;
        public float CooldownTimeModifier = 1f;
    }
}