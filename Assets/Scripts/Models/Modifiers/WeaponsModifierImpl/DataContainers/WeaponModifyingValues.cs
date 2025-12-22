using System;
using UnityEngine.Serialization;

namespace Models.Modifiers.WeaponsModifierImpl.DataContainers
{
    [Serializable]
    public class WeaponModifyingValues
    {
        public float ReloadTimeModifier = 1f;
        public float SpreadModifier = 1f;
        [FormerlySerializedAs("DamageModifier")] public float DamageToDealModifier = 1f;
        public float SpeedModifier = 1f;
        public float CooldownTimeModifier = 1f;
    }
}