using System;
using System.Collections.Generic;
using Core;
using Models.Items.Base.Enums;
using Models.Items.Base.ScriptableObjects;
using Models.WorldObjects.Creatures.PlayerImpl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UserInterface.Menus.CastingMenuImplementation;
using UserInterface.Menus.SelectionMenuImplementation;
using Utils;
using Zenject;
using Random = UnityEngine.Random;

namespace Models.Items.Base.Spawners
{
    /// <summary>
    /// This class provides a possibility for player to select one modifier
    /// from several randomly loaded ones after solving a puzzle.
    /// </summary>
    public class ItemsSpawner : MonoBehaviour
    {
        [Header("Dependencies")]
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
        private DiContainer _diContainer;
        private Player _player;

        [Inject]
        private void Construct(DiContainer diContainer, Player player)
        {
            _diContainer = diContainer;
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
        
        /// <summary>
        /// Opens casting puzzle menu, and if the puzzle is solved,
        /// opens the selection menu with several randomly loaded artefacts.
        /// </summary>
        public void RunArtefactSelectionProcess(ItemsLoadType loadType)
        {
            _castingMenu.OpenCastingPuzzleThenIfSolved(() =>
            {
                LoadArtefacts(loadType, artefacts => 
                    _selectionMenu.OpenMenuToSelect(artefacts, SpawnSelectedArtefact));
            });
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
        
        private void LoadArtefacts(ItemsLoadType loadType, Action<List<SelectableItemSo>> onLoadedCallback)
        {
            var loadedArtefacts = new List<SelectableItemSo>();

            for (int i = 0; i < _countToLoad; i++)
            {
                // Decide which rarity to use. 
                string chosenLabel;
                switch (loadType)
                {
                    case ItemsLoadType.Random:
                        chosenLabel = _commonArtefactsLabel;
                        // chosenLabel = GetRandomLabelTroughRarity();
                        break;
                    case ItemsLoadType.OnlyGolden:
                        chosenLabel = _goldArtefactsLabel;
                        break;
                    case ItemsLoadType.OnlyIncredible:
                        chosenLabel = _incredibleArtefactsLabel;
                        break;
                    default:
                        Debug.LogError($"Unknown load type: {loadType}. Defaulting to Random.");
                        chosenLabel = GetRandomLabelTroughRarity();
                        break;
                }
                        
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
                            // _selectionMenu.OpenMenuToSelect(loadedArtefacts, SpawnSelectedModifier);
                            onLoadedCallback?.Invoke(loadedArtefacts);
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

        private void SpawnSelectedArtefact(SelectableItemSo selectedArtefact)
        {
            var prefab = _diContainer
                .InstantiatePrefab(selectedArtefact.PrefabToSpawn, _player.transform);
            prefab.transform.localPosition = Vector3.zero;
        }
    }
}