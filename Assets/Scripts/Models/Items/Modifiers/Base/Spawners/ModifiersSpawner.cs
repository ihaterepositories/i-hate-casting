using System.Collections.Generic;
using Core;
using Models.Items.Base.Enums;
using Models.Items.Base.ScriptableObjects;
using Models.Items.Base.Spawners;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UserInterface.Menus.CastingMenuImplementation;
using Utils;
using Random = UnityEngine.Random;

namespace Models.Items.Modifiers.Base.Spawners
{
    public class ModifiersSpawner : ItemsSpawner
    {
        [SerializeField] private CastingMenu _castingMenu;
        [SerializeField] private string _commonModifiersAddressablesLabel;
        [SerializeField] private string _goldModifiersAddressablesLabel;
        [SerializeField] private string _incredibleModifiersAddressablesLabel;
        
        private List<SelectableItemSo> _modifiers = new();
        private readonly int _countToLoad = AppConstants.MaxItemsToSelectPerOneSelectionEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _castingMenu.OpenCastingPuzzleThen(LoadModifiers);
        }
        
        private void LoadModifiers()
        {
            _modifiers.Clear();

            for (int i = 0; i < _countToLoad; i++)
            {
                // Change after adding new modifiers.
                string chosenLabel = _commonModifiersAddressablesLabel;
                // string chosenLabel = GetLabel();
                
                var locationsLoader = Addressables.LoadResourceLocationsAsync(chosenLabel, typeof(SelectableItemSo));
                locationsLoader.Completed += locationsLoaderHandle =>
                {
                    if (locationsLoaderHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        var resourceLocations = locationsLoaderHandle.Result;
                        if (resourceLocations.Count > 0)
                        {
                            LoadModifierFrom(resourceLocations);
                        }
                    }
                    else
                    {
                        Debug.LogError($"Failed to load resource locations with {chosenLabel} label: {locationsLoaderHandle.OperationException}");
                    }
                };
            }
        }

        private void LoadModifierFrom(IList<IResourceLocation> resourceLocations)
        {
            int randomIndex = Random.Range(0, resourceLocations.Count);
            var assetLoader = Addressables.LoadAssetAsync<SelectableItemSo>(resourceLocations[randomIndex]);
            assetLoader.Completed += assetLoaderHandle =>
            {
                if (assetLoaderHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    _modifiers.Add(assetLoaderHandle.Result);
                    Debug.Log($"Selected {assetLoaderHandle.Result.name}");
                    if (_modifiers.Count == _countToLoad)
                    {
                        ShowSelectionFor(_modifiers);
                    }
                }
            };
        }

        private string GetLabel()
        {
            var rarity = RarityPicker.Generate();

            switch (rarity)
            {
                case ItemRarity.Common:
                    return _commonModifiersAddressablesLabel;
                case ItemRarity.Gold:
                    return _goldModifiersAddressablesLabel;
                case ItemRarity.Incredible:
                    return _incredibleModifiersAddressablesLabel;
                default:
                    Debug.LogError($"Unknown rarity: {rarity}");
                    return _commonModifiersAddressablesLabel;
            }
        }
    }
}