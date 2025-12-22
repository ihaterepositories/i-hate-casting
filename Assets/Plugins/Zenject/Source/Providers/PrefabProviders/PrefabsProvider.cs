#if !NOT_UNITY3D

using ModestTree;

namespace Zenject
{
    [NoReflectionBaking]
    public class PrefabsProvider : IPrefabProvider
    {
        readonly UnityEngine.Object _prefab;

        public PrefabsProvider(UnityEngine.Object prefab)
        {
            Assert.IsNotNull(prefab);
            _prefab = prefab;
        }

        public UnityEngine.Object GetPrefab(InjectContext _)
        {
            return _prefab;
        }
    }
}

#endif


