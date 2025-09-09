using System;
using Models.Items.Weapons.Base.StatsHandling;
using Models.Pooling;
using UnityEngine;

namespace Models.Items.Weapons.Bullets.Base
{
    public class Bullet : PoolAbleMonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rb;
        
        private float LifeTime => _firedFromWeaponStatsCalculator.GetRange() / _firedFromWeaponStatsCalculator.GetSpeed();
        private float _lifeTimeLeft;
        
        protected WeaponStatsCalculator _firedFromWeaponStatsCalculator;
        
        public float DamageToDeal => _firedFromWeaponStatsCalculator.GetDamageToDeal();

        private void Update()
        {
            Move();
            ReduceLifeTime();
            DestroyOnLifeTimeExpire();
        }

        protected virtual void Move()
        {
            _rb.velocity = transform.right * _firedFromWeaponStatsCalculator.GetSpeed();
        }

        private void ReduceLifeTime()
        {
            _lifeTimeLeft -= Time.deltaTime;
        }
        
        private void DestroyOnLifeTimeExpire()
        {
            if (_lifeTimeLeft <= 0)
            {
                _lifeTimeLeft = LifeTime;
                ReturnToPool();
            }
        }

        public void Init(WeaponStatsCalculator weaponStatsCalculator)
        {
            _firedFromWeaponStatsCalculator = weaponStatsCalculator;
            _lifeTimeLeft = LifeTime;
        }
    }
}