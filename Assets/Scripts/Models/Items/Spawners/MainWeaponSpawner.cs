using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using Models.Items.Spawners.Base;
using UnityEngine;

namespace Models.Items.Spawners
{
    // Provides a base weapon selection for the player on the start of the game.
    public class MainWeaponSpawner : ItemSpawner
    {
        [SerializeField] private List<SelectableItemSo> _mainWeaponPrefabs;

        private void Start()
        {
            ShowSelectionFor(_mainWeaponPrefabs);
        }
    }
}