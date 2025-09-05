using Models.Items.Artefacts.Modifiers.Implementations.WeaponsModifierImplementation.ScriptableObjects.SerializingDataContainers;
using UnityEngine;

namespace Models.Items.Artefacts.Modifiers.Implementations.WeaponsModifierImplementation.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponsModifierStats", menuName = "ScriptableObjects/WeaponsModifierStats")]
    public class WeaponsModifierStatsSo : ScriptableObject
    {
        public WeaponModifyingValues PlayerShortRangeWeaponModifyingValues;
        public WeaponModifyingValues PlayerMediumRangeWeaponModifyingValues;
        public WeaponModifyingValues PlayerLongRangeWeaponModifyingValues;
        public WeaponModifyingValues DefaultEnemyWeaponModifyingValues;
        public WeaponModifyingValues BossWeaponModifyingValues;
    }
}