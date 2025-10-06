using UnityEngine;
using Zenject;

namespace Models.WorldObjects.Base.Pooling
{
    public class PoolContainer<T>  : MonoBehaviour where T : PoolableMonoBehaviour
    {
        [SerializeField] private T _prefab;
        
        private ObjectPool<T> _pool;
        private DiContainer _diContainer;
        
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        private void Awake()
        {
            _pool = new ObjectPool<T>(_prefab, _diContainer, this.transform);
        }

        public T GetFreeObject()
        {
            var freeObject = _pool.GetFreeObject();
            return freeObject as T;
        }
    }
}