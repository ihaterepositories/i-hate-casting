using Core.Pausing.Interfaces;
using Models.Bullets.Dtos;
using Models.Bullets.Services.LifeTimeCalculating.Factories;
using Models.Bullets.Services.LifeTimeCalculating.Interfaces;
using Models.Bullets.Services.Moving.Factories;
using Models.Bullets.Services.Moving.Interfaces;
using Models.Creatures.Enums;
using Systems.Combat.Dtos;
using Systems.Combat.Interfaces;
using Systems.Pooling.Models;
using UnityEngine;
using Zenject;

namespace Models.Bullets
{
    // Bullet don`t have personal stats. It gets them from a weapon.
    public class Bullet : PoolableMonoBehaviour, IConfigurable<BulletConfig>
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        // Factories
        private BulletMoversFactory _bulletMoversFactory;
        private BulletLifeTimeCalculatorsFactory _bulletLifeTimeCalculatorsFactory;
        
        // Services
        private IBulletMoveService _mover;
        private IBulletLifeTimeCalculateService _lifeTimeCalculator;
        
        // Local logic
        private CreatureType _bulletOwner;
        private float _damageToDeal;
        private bool _isLaunched;
        private IPauser _pauser;

        [Inject]
        private void Construct(
            BulletMoversFactory bulletMoversFactory,
            BulletLifeTimeCalculatorsFactory bulletLifeTimeCalculatorsFactory,
            IPauser pauser)
        {
            _bulletMoversFactory = bulletMoversFactory;
            _bulletLifeTimeCalculatorsFactory = bulletLifeTimeCalculatorsFactory;
            _pauser = pauser;
        }

        private void Update()
        {
            if (_pauser.IsGamePaused) return;
            if (!_isLaunched) return;
            
            _mover.EnableMove();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_pauser.IsGamePaused) return;
            
            if (other.collider.TryGetComponent(out Bullet otherBullet))
            {
                if (otherBullet._bulletOwner == _bulletOwner) return;
            }
            
            if (other.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeHit(new DamageInfo(_damageToDeal));
            }
            
            Destroy();
        }

        public void Configure(BulletConfig bulletConfig)
        {
            _bulletOwner = bulletConfig.BulletOwner;
            _mover = _bulletMoversFactory.Create(bulletConfig.MoveType, _rb, transform, 0);
            _lifeTimeCalculator = _bulletLifeTimeCalculatorsFactory.Create(bulletConfig.LifeTimeCalculatorType, 0f, 0f);
            _spriteRenderer.sprite = bulletConfig.Sprite;
        }

        public void Launch(BulletLaunchData launchData)
        {
            _mover.UpdateSpeed(launchData.MoveSpeed);
            _lifeTimeCalculator.UpdateLifeTimeLimit(launchData.MoveSpeed, launchData.MoveRange);
            _lifeTimeCalculator.OnTimeLimitReached += Destroy;
            _damageToDeal = launchData.DamageToDeal;
            
            transform.position = launchData.LaunchPosition;
            transform.rotation = Quaternion.Euler(0, 0, launchData.RotationAngle + launchData.SpreadAngle);
            
            _lifeTimeCalculator.StartCalculate();
            _isLaunched = true;
        }

        private void Destroy()
        {
            _isLaunched = false;
            _lifeTimeCalculator.OnTimeLimitReached -= Destroy;
            ReturnToPool();
        }
    }
}