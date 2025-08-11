using System.Collections.Generic;
using Core.ItemSpawners.Base;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.ItemSpawners
{
    public class DefaultWeaponSpawner : ItemSpawner
    {
        [FormerlySerializedAs("defaultWeaponPrefabs")] [SerializeField] private List<SelectableItemSo> _defaultWeaponPrefabs;
        
        public void ShowSelection()
        {
            ShowSelectionFor(_defaultWeaponPrefabs);
        }
    }
}