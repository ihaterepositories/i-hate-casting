using System.Collections.Generic;
using Core.AssetsLoading.PrefabsProviders.Interfaces;
using Systems.ResourcesCleaning.Interfaces;

namespace Core
{
    public class ResourcesCleaner
    {
        private readonly List<IResourceCleanable> _resourceCleanables = new();
        
        public ResourcesCleaner(
            IPrefabsProvider prefabsProvider)
        {
            _resourceCleanables.Add(prefabsProvider);
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