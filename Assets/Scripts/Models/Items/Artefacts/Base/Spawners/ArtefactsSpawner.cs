using System;
using System.Collections.Generic;
using Core;
using Models.Creatures.Implementations.PlayerImplementation;
using Models.Items.Base.Enums;
using Models.Items.Base.ScriptableObjects;
using Models.Spawning;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UserInterface.Menus.CastingMenuImplementation;
using UserInterface.Menus.SelectionMenuImplementation;
using Utils;
using Zenject;
using Random = UnityEngine.Random;

namespace Models.Items.Artefacts.Base.Spawners
{
    // Every time when player is ???, this class provides a possibility
    // to select one modifier from several randomly loaded ones after solving a puzzle.
    public class ArtefactsSpawner : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObjectsSpawner _gameObjectsSpawner;
        [SerializeField] private CastingMenu _castingMenu;
        [SerializeField] private SelectionMenu _selectionMenu;
        
        [Header("Addressables labels of Artefacts")]
        [SerializeField] private string _commonArtefactsLabel;
        [SerializeField] private string _goldArtefactsLabel;
        [SerializeField] private string _incredibleArtefactsLabel;
        
        private List<IResourceLocation> _preloadedCommonArtefactsLocations;
        private List<IResourceLocation> _preloadedGoldArtefactsLocations;
        private List<IResourceLocation> _preloadedIncredibleArtefactsLocations;
        
        private readonly int _countToLoad = AppConstants.MaxItemsToSelectPerOneSelectionEvent;
        private Player _player;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            LoadAddressablesLocationsWith(_commonArtefactsLabel, loadedLocations =>
            {
                _preloadedCommonArtefactsLocations = loadedLocations;
            });
            
            LoadAddressablesLocationsWith(_goldArtefactsLabel, loadedLocations =>
            {
                _preloadedGoldArtefactsLocations = loadedLocations;
            });
            
            LoadAddressablesLocationsWith(_incredibleArtefactsLabel, loadedLocations =>
            {
                _preloadedIncredibleArtefactsLocations = loadedLocations;
            });
        }

        private void Update()
        {
            // For testing purposes only.
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _castingMenu.OpenCastingPuzzleThenIfSolved(LoadArtefacts);
        }

        private void LoadAddressablesLocationsWith(string addressablesLabel, Action<List<IResourceLocation>> onReturnLoadedLocations)
        {
            var locationsLoader = Addressables.LoadResourceLocationsAsync(addressablesLabel, typeof(SelectableItemSo));

            locationsLoader.Completed += locationsLoaderHandle =>
            {
                if (locationsLoaderHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    var resourceLocations = locationsLoaderHandle.Result;

                    if (resourceLocations.Count <= 0)
                    {
                        Debug.LogError($"No resource locations found with {addressablesLabel} label. " +
                                       $"Check if there are any assets with that label.");
                        onReturnLoadedLocations?.Invoke(new List<IResourceLocation>());
                    }
                    else
                    {
                        onReturnLoadedLocations?.Invoke(new List<IResourceLocation>(resourceLocations));
                    }
                }
                else
                {
                    Debug.LogError($"Failed to load resource locations with {addressablesLabel} label.");
                    onReturnLoadedLocations?.Invoke(new List<IResourceLocation>());
                }
            };
        }

        
        private void LoadArtefacts()
        {
            var loadedArtefacts = new List<SelectableItemSo>();

            for (int i = 0; i < _countToLoad; i++)
            {
                // Change after adding new modifiers.
                string chosenLabel = _commonArtefactsLabel;
                // string chosenLabel = GetLabel();
                        
                // Decide which preloaded locations to use for chosen label.
                var resourceLocations = chosenLabel switch
                {
                    _ when chosenLabel == _commonArtefactsLabel => _preloadedCommonArtefactsLocations,
                    _ when chosenLabel == _goldArtefactsLabel => _preloadedGoldArtefactsLocations,
                    _ when chosenLabel == _incredibleArtefactsLabel => _preloadedIncredibleArtefactsLocations,
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                // Load a random modifier.
                int randomIndex = Random.Range(0, resourceLocations.Count);
                var assetLoader = Addressables.LoadAssetAsync<SelectableItemSo>(resourceLocations[randomIndex]);
                assetLoader.Completed += assetLoaderHandle =>
                {
                    if (assetLoaderHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        var loadedModifier = assetLoaderHandle.Result;
                        loadedArtefacts.Add(loadedModifier);
                        
                        // Open selection menu when all artefacts are loaded.
                        if (loadedArtefacts.Count == _countToLoad)
                        {
                            _selectionMenu.OpenMenuToSelect(loadedArtefacts, SpawnSelectedModifier);
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to load asset.");
                    }
                };
            }
        }

        private string GetRandomLabelTroughRarity()
        {
            var rarity = RarityPicker.Generate();

            switch (rarity)
            {
                case ItemRarity.Common:
                    return _commonArtefactsLabel;
                case ItemRarity.Gold:
                    return _goldArtefactsLabel;
                case ItemRarity.Incredible:
                    return _incredibleArtefactsLabel;
                default:
                    Debug.LogError($"Unknown rarity: {rarity}");
                    return _commonArtefactsLabel;
            }
        }
        
        private void SpawnSelectedModifier(SelectableItemSo modifierSo)
        {
            _gameObjectsSpawner.Spawn(modifierSo.PrefabToSpawn, _player.transform);
        }
    }
}