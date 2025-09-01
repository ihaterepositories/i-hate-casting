using System;
using Models.Items.Weapons.Base.StatsHandling;
using Pooling.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models.Items.Bullets.Base
{
    public abstract class Bullet : MonoBehaviour, IPoolAble
    {
        [FormerlySerializedAs("rb")] [SerializeField] protected Rigidbody2D _rb;
        
        private float LifeTime => _firedFromWeaponStatsCalculator.GetRange() / _firedFromWeaponStatsCalculator.GetSpeed();
        private float _lifeTimeLeft;
        
        protected WeaponStatsCalculator _firedFromWeaponStatsCalculator;
        
        public GameObject GameObject => gameObject;
        public float Damage => _firedFromWeaponStatsCalculator.GetDamage();
        
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

        public void Init(WeaponStatsCalculator weaponStatsCalculator)
        {
            _firedFromWeaponStatsCalculator = weaponStatsCalculator;
            _lifeTimeLeft = LifeTime;
        }
    }
}