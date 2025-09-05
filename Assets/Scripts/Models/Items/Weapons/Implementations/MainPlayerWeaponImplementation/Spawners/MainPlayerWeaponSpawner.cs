using System;
using System.Collections.Generic;
using Models.Creatures.Implementations.PlayerImplementation;
using Models.Items.Base.ScriptableObjects;
using Models.Spawning;
using UnityEngine;
using UserInterface.Menus.SelectionMenuImplementation;
using Zenject;

namespace Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation.Spawners
{
    // Provides a base weapon selection for the player on the start of the game.
    public class MainPlayerWeaponSpawner : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObjectsSpawner _gameObjectsSpawner;
        [SerializeField] private SelectionMenu _selectionMenu;
        
        [Header("Main player`s weapon to choose from on the start of the game")]
        [SerializeField] private List<SelectableItemSo> _mainWeapon;
        
        private Transform _playerTransform;
        
        public static event Action<PlayerWeapon> OnSpawned;
        
        [Inject]
        private void Construct(Player player)
        {
            _playerTransform = player.transform;
        }

        private void Start()
        {
            _selectionMenu.OpenMenuToSelect(_mainWeapon, SpawnSelectedWeapon);
        }
        
        private void SpawnSelectedWeapon(SelectableItemSo selectedItem)
        {
            _gameObjectsSpawner.Spawn(selectedItem.PrefabToSpawn, _playerTransform, OnMainPlayerWeaponSpawned);
        }
        
        private void OnMainPlayerWeaponSpawned(GameObject weapon)
        {
            var playerWeapon = weapon.GetComponent<PlayerWeapon>();
            OnSpawned?.Invoke(playerWeapon);
        }
    }
}