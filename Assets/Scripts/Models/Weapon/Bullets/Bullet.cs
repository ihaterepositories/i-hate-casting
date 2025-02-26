using System;
using Models.Weapon.Bullets.Data;
using Pooling.Interfaces;
using UnityEngine;

namespace Models.Weapon.Bullets
{
    public abstract class Bullet : MonoBehaviour, IPoolAble
    {
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected BulletStats stats;

        private float _lifeTimeLeft;
        
        public GameObject GameObject => gameObject;
        public event Action<IPoolAble> OnDestroyed;

        private void Start()
        {
            _lifeTimeLeft = stats.lifeTime;
        }

        private void Update()
        {
            Move();
            ReduceLifeTime();
            DestroyOnLifeTimeExpire();
        }

        protected abstract void Move();

        private void ReduceLifeTime()
        {
            _lifeTimeLeft -= Time.deltaTime;
        }
        
        private void DestroyOnLifeTimeExpire()
        {
            if (_lifeTimeLeft <= 0)
            {
                Reset();
                _lifeTimeLeft = stats.lifeTime;
            }
        }
        
        public void Reset()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}