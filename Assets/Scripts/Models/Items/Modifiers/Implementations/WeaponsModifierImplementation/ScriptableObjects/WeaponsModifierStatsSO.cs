using Models.Items.Modifiers.Implementations.WeaponsModifierImplementation.Models;
using UnityEngine;

namespace Models.Items.Modifiers.Implementations.WeaponsModifierImplementation.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ModifierStats", menuName = "ScriptableObjects/ModifierStats")]
    public class WeaponsModifierStatsSO : ScriptableObject
    {
        public ModifiableWeaponStats PlayerShortRangeModifiableWeaponStats;
        public ModifiableWeaponStats PlayerMediumRangeModifiableWeaponStats;
        public ModifiableWeaponStats PlayerLongRangeModifiableWeaponStats;
    }
}