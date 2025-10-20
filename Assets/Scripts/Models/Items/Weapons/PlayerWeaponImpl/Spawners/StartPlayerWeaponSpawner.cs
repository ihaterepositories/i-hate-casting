using System;
using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using Models.WorldObjects.Creatures.PlayerImpl;
using UnityEngine;
using UserInterface.Menus.SelectionMenuImplementation;
using Zenject;

namespace Models.Items.Weapons.PlayerWeaponImpl.Spawners
{
    // Provides a base weapon selection for the player on the start of the game.
    public class StartPlayerWeaponSpawner : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private SelectionMenu _selectionMenu;
        
        [Header("Main player`s weapon to choose from on the start of the game")]
        [SerializeField] private List<SelectableItemSo> _mainWeapon;
        
        private DiContainer _diContainer;
        private Transform _playerTransform;
        
        public static event Action<PlayerWeapon> OnSpawned;
        
        [Inject]
        private void Construct(DiContainer container, Player player)
        {
            _diContainer = container;
            _playerTransform = player.transform;
        }

        private void Start()
        {
            _selectionMenu.OpenMenuToSelect(_mainWeapon, SpawnSelectedWeapon);
        }
        
        private void SpawnSelectedWeapon(SelectableItemSo selectedWeapon)
        {
            var prefab = _diContainer
                .InstantiatePrefab(selectedWeapon.PrefabToSpawn, _playerTransform)
                .GetComponent<PlayerWeapon>();
            prefab.transform.localPosition = Vector3.zero;
            OnSpawned?.Invoke(prefab);
        }
    }
}