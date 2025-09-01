using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using Models.Items.Base.Spawners;
using UnityEngine;

namespace Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation.Spawners
{
    // Provides a base weapon selection for the player on the start of the game.
    public class MainPlayerWeaponSpawner : ItemsSpawner
    {
        [SerializeField] private List<SelectableItemSo> _mainWeapon;

        private void Start()
        {
            ShowSelectionFor(_mainWeapon);
        }
    }
}