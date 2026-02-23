using Core.Pausing.Interfaces;
using Models.Bullets.Dtos;
using Models.Bullets.Services.LifeTimeCalculating.Enums;
using Models.Bullets.Services.LifeTimeCalculating.Factories;
using Models.Bullets.Services.LifeTimeCalculating.Interfaces;
using Models.Bullets.Services.Moving.Enums;
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
    // Some bullet stats comes from weapon, from which bullet was launched. 
    // This can be seen in the Launch method.
    public class Bullet : PoolableMonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D _rb;
        
        [Header("Settings")]
        [SerializeField] private CreatureType _bulletOwner;
        [SerializeField] private BulletMoveType _bulletMoveType;
        [SerializeField] private BulletLifeTimeCalculatorType _bulletLifeTimeCalculatorType;
        
        private IBulletMoveService _mover;
        private IBulletLifeTimeCalculateService _lifeTimeCalculator;
        
        private float _damageToDeal;
        private bool _isLaunched;
        private IPauser _pauser;

        [Inject]
        private void Construct(
            BulletMoversFactory bulletMoversFactory,
            BulletLifeTimeCalculatorsFactory bulletLifeTimeCalculatorsFactory,
            IPauser pauser)
        {
            _mover = bulletMoversFactory.Create(_bulletMoveType, _rb, transform, 0);
            _lifeTimeCalculator = bulletLifeTimeCalculatorsFactory.Create(_bulletLifeTimeCalculatorType, 0f, 0f);
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