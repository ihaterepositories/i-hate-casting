using System;
using System.Collections.Generic;
using Core.AssetsLoading.PrefabsProviders.Interfaces;
using Systems.ResourcesCleaning.Interfaces;
using UnityEngine;
using Zenject;

namespace Core
{
    /// <summary>
    /// Clean resources of all registered resource cleanables inside through Construct or AddResourceCleanable methods.
    /// </summary>
    public class ResourcesCleaner : MonoBehaviour
    {
        private readonly List<IResourceCleanable> _resourceCleanables = new();

        [Inject]
        private void Construct(IAssetsLoader assetsLoader)
        {
            _resourceCleanables.Add(assetsLoader);
        }

        private void OnDisable()
        {
            CleanResources();
        }

        public void AddResourceCleanable(IResourceCleanable resourceCleanable)
        {
            _resourceCleanables.Add(resourceCleanable);
        }

        public void CleanResources()
        {
            foreach (var resourceCleanable in _resourceCleanables)
            {
                resourceCleanable.CleanResources();
            }
        }
    }
}