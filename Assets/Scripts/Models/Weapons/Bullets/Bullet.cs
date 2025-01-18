using UnityEngine;
using UnityEngine.Serialization;

namespace Models.Weapons.Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb;
        
        private int _damage;
        protected float Speed = 10;
        private float _lifeTime = 3;
        
        public void SetStats(int damage, int speed, float lifeTime)
        {
            _damage = damage;
            Speed = speed;
            _lifeTime = lifeTime;
        }
        
        private void Update()
        {
            Move();
            CheckLifeTime();
        }

        protected abstract void Move();
        
        private void CheckLifeTime()
        {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0)
                Destroy(gameObject);
        }
    }
}