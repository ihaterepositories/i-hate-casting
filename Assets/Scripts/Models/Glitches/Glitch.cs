using Models.Creatures.Dtos;
using Models.Creatures.Enums;
using Models.Creatures.Services.StatsMultiplying.Interfaces;
using Models.Creatures.Services.StatsMultiplying.Providers;
using Models.Glitches.Dtos;
using Models.Weapons.Dtos;
using Models.Weapons.Enums;
using Models.Weapons.Services.StatsMultiplying.Interfaces;
using Models.Weapons.Services.StatsMultiplying.Providers;
using UnityEngine;
using Zenject;

namespace Models.Glitches
{
    /// <summary>
    /// Modifies creature`s or weapon`s stats by adding values to their stats multipliers.
    /// </summary>
    public class Glitch : MonoBehaviour
    {
        [SerializeField] private GlitchStats _glitchStats;
        
        private (IWeaponStatsMultipliers multiplier, WeaponStats modifyingValues)[] _weaponsModifyingData;
        private (ICreatureStatsMultipliers multiplier, CreatureStats modifyingValues)[] _creaturesModifyingData;
        
        [Inject]
        private void Construct(
            WeaponStatsMultipliersProvider weaponsStatsMultipliersProvider,
            CreatureStatsMultipliersProvider creatureStatsMultipliersProvider)
        {
            // Assigns "add values" to their multipliers
            // (add values - values which will be added to current multiplier`s values)
            
            _weaponsModifyingData = new[]
            {
                (weaponsStatsMultipliersProvider.GetFor(WeaponType.PlayerWeapon),_glitchStats.PlayerWeaponModifiers),
                (weaponsStatsMultipliersProvider.GetFor(WeaponType.EnemyWeapon), _glitchStats.EnemyWeaponsModifiers),
                (weaponsStatsMultipliersProvider.GetFor(WeaponType.BossWeapon),_glitchStats.BossWeaponsModifiers)
            };
            
            _creaturesModifyingData = new[]
            {
                (creatureStatsMultipliersProvider.GetFor(CreatureType.Player),_glitchStats.PlayerModifiers),
                (creatureStatsMultipliersProvider.GetFor(CreatureType.Enemy), _glitchStats.EnemiesModifiers),
                (creatureStatsMultipliersProvider.GetFor(CreatureType.Boss), _glitchStats.BossesModifiers)
            };
            
            Activate();
        }

        private void OnDisable()
        {
            Deactivate();
        }

        private void Activate()
        {
            foreach (var (multiplier, stats) in _weaponsModifyingData)
            {
                multiplier.AddValuesToMultipliers(stats);
            }
            
            foreach (var (multiplier, stats) in _creaturesModifyingData)
            {
                multiplier.AddValuesToMultipliers(stats);
            }
        }

        private void Deactivate()
        {
            foreach (var (multiplier, stats) in _weaponsModifyingData)
            {
                multiplier.SubtractValuesFromMultipliers(stats);
            }
            
            foreach (var (multiplier, stats) in _creaturesModifyingData)
            {
                multiplier.SubtractValuesFromMultipliers(stats);
            }
        }
    }
}