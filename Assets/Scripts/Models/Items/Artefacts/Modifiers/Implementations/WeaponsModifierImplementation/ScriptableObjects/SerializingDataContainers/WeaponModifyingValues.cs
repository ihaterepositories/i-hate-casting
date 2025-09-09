using System;
using UnityEngine.Serialization;

namespace Models.Items.Artefacts.Modifiers.Implementations.WeaponsModifierImplementation.ScriptableObjects.SerializingDataContainers
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