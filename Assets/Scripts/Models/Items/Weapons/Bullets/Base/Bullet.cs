using Models.Items.Weapons.Base.StatsHandling;
using Models.Pooling;
using UnityEngine;

namespace Models.Items.Weapons.Bullets.Base
{
    public abstract class Bullet : PoolAbleMonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rb;
        
        private float LifeTime => _firedFromWeaponStatsCalculator.GetRange() / _firedFromWeaponStatsCalculator.GetSpeed();
        private float _lifeTimeLeft;
        
        protected WeaponStatsCalculator _firedFromWeaponStatsCalculator;
        
        public float Damage => _firedFromWeaponStatsCalculator.GetDamage();

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