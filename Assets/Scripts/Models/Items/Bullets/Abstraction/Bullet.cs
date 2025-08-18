using System;
using Models.Items.Weapons.Base.ScriptableObjects;
using Pooling.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models.Items.Bullets.Abstraction
{
    public abstract class Bullet : MonoBehaviour, IPoolAble
    {
        [FormerlySerializedAs("rb")] [SerializeField] protected Rigidbody2D _rb;
        
        private float LifeTime => _firedFromWeaponStatsSo.GetRange() / _firedFromWeaponStatsSo.GetSpeed();
        private float _lifeTimeLeft;
        
        protected WeaponStatsSo _firedFromWeaponStatsSo;
        
        public GameObject GameObject => gameObject;
        public float Damage => _firedFromWeaponStatsSo.GetDamage();
        
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

        public void Init(WeaponStatsSo statsSo)
        {
            _firedFromWeaponStatsSo = statsSo;
            _lifeTimeLeft = LifeTime;
        }
    }
}