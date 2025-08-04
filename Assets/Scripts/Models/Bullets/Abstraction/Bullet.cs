using System;
using Models.Weapons.Base;
using PoolingCore.Interfaces;
using UnityEngine;

namespace Models.Bullets.Abstraction
{
    public abstract class Bullet : MonoBehaviour, IPoolAble
    {
        [SerializeField] protected Rigidbody2D rb;
        
        private float LifeTime => FiredFromWeaponStats.GetRange() / FiredFromWeaponStats.GetSpeed();
        private float _lifeTimeLeft;
        
        protected WeaponStats FiredFromWeaponStats;
        
        public GameObject GameObject => gameObject;
        public float Damage => FiredFromWeaponStats.GetDamage();
        
        public event Action<IPoolAble> OnDestroyed;

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
            if (_lifeTimeLeft <= 0) Reset();
        }
        
        public void Reset()
        {
            OnDestroyed?.Invoke(this);
            _lifeTimeLeft = LifeTime;
        }

        public void Init(WeaponStats stats)
        {
            FiredFromWeaponStats = stats;
            _lifeTimeLeft = LifeTime;
        }
    }
}