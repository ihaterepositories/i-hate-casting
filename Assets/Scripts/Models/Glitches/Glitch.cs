using Models.Creatures.Dtos;
using Models.Creatures.Enums;
using Models.Creatures.Services.StatsScalers.Interfaces;
using Models.Creatures.Services.StatsScalers.Providers;
using Models.Glitches.Dtos;
using Models.Weapons.Dtos;
using Models.Weapons.Enums;
using Models.Weapons.Services.StatsScalers.Interfaces;
using Models.Weapons.Services.StatsScalers.Providers;
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
        
        private (IWeaponStatsScaler multiplier, WeaponStats modifyingValues)[] _weaponsModifyingData;
        private (ICreatureStatsScaler multiplier, CreatureStats modifyingValues)[] _creaturesModifyingData;
        
        [Inject]
        private void Construct(
            WeaponStatsScalersProvider weaponsStatsScalersProvider,
            CreatureStatsScalersProvider creatureStatsScalersProvider)
        {
            // Assigns "add values" to their multipliers
            // (add values - values which will be added to current multiplier`s values)
            
            _weaponsModifyingData = new[]
            {
                (weaponsStatsScalersProvider.GetFor(WeaponType.PlayerWeapon),_glitchStats.PlayerWeaponModifiers),
                (weaponsStatsScalersProvider.GetFor(WeaponType.EnemyWeapon), _glitchStats.EnemyWeaponsModifiers),
                (weaponsStatsScalersProvider.GetFor(WeaponType.BossWeapon),_glitchStats.BossWeaponsModifiers)
            };
            
            _creaturesModifyingData = new[]
            {
                (creatureStatsScalersProvider.GetFor(CreatureType.Player),_glitchStats.PlayerModifiers),
                (creatureStatsScalersProvider.GetFor(CreatureType.Enemy), _glitchStats.EnemiesModifiers),
                (creatureStatsScalersProvider.GetFor(CreatureType.Boss), _glitchStats.BossesModifiers)
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
                multiplier.AddMultipliers(stats);
            }
            
            foreach (var (multiplier, stats) in _creaturesModifyingData)
            {
                multiplier.AddMultipliers(stats);
            }
        }

        private void Deactivate()
        {
            foreach (var (multiplier, stats) in _weaponsModifyingData)
            {
                multiplier.RemoveMultipliers(stats);
            }
            
            foreach (var (multiplier, stats) in _creaturesModifyingData)
            {
                multiplier.RemoveMultipliers(stats);
            }
        }
    }
}