using Core.Pausing.Interfaces;
using Models.Creatures.Dtos;
using Models.Creatures.Enums;
using Models.Creatures.Services.Animating.Enums;
using Models.Creatures.Services.Animating.Factories;
using Models.Creatures.Services.Animating.Interfaces;
using Models.Creatures.Services.Destroying.Enums;
using Models.Creatures.Services.Destroying.Factories;
using Models.Creatures.Services.Destroying.Interfaces;
using Models.Creatures.Services.Living.Enums;
using Models.Creatures.Services.Living.Factories;
using Models.Creatures.Services.Living.Interfaces;
using Models.Creatures.Services.MoveBoosting.Enums;
using Models.Creatures.Services.MoveBoosting.Factories;
using Models.Creatures.Services.MoveBoosting.Interfaces;
using Models.Creatures.Services.Moving.Enums;
using Models.Creatures.Services.Moving.Factories;
using Models.Creatures.Services.Moving.Interfaces;
using Models.Creatures.Services.ObstaclesBypassing.Enums;
using Models.Creatures.Services.ObstaclesBypassing.Factories;
using Models.Creatures.Services.StatsCalculating.Factories;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using Systems.Combat.Dtos;
using Systems.Combat.Interfaces;
using Systems.Pooling.Models;
using UnityEngine;
using Zenject;

namespace Models.Creatures
{
    public class Creature : PoolableMonoBehaviour, IDamageable
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        
        [Header("Behaviour settings")]
        [SerializeField] private CreatureType _creatureType;
        [SerializeField] private CreatureHealthType _healthType;
        [SerializeField] private CreatureMoveType _moveType;
        [SerializeField] private CreatureObstaclesBypassType _obstaclesBypassType;
        [SerializeField] private CreatureMoveBoostType _moveBoostType;
        [SerializeField] private CreatureDestroyType _destroyType;
        [SerializeField] private CreatureAnimatingType _animatingType;
        
        [Header("Stats")]
        [SerializeField] private CreatureStats _stats;

        [Header("View settings")] 
        [SerializeField] private AnimatorOverrideController _animatorOverrideController;
        
        // Services
        private ICreatureStatsCalculator _statsCalculator;
        private ICreatureHealth _health;
        private ICreatureMover _mover;
        private ICreatureMoveBooster _moveBooster;
        private ICreatureDestroyer _destroyer;
        private ICreatureAnimationLauncher _animationLauncher;
        private IPauser _pauser;
        
        // Local logic
        private bool _isSpriteFlipped;

        [Inject]
        private void Construct(
            CreatureStatsCalculatorsFactory statsCalculatorsFactory,
            CreatureHealthServicesFactory healthServicesFactory,
            CreatureMoversFactory moversFactory,
            CreatureObstaclesBypassersFactory obstaclesBypassersFactory,
            CreatureMoveBoostersFactory moveBoostersFactory,
            CreatureDestroyersFactory destroyersFactory,
            CreatureAnimationLaunchersFactory animationLaunchersFactory,
            IPauser pauser)
        {
            _statsCalculator = statsCalculatorsFactory.Create(_creatureType, _stats);
            
            _health = healthServicesFactory.Create(_healthType, _statsCalculator);
            _health.OnHealthGone += Destroy;
            
            _mover = moversFactory.Create(_moveType, _statsCalculator, _rigidbody2D, transform);
            
            if (_obstaclesBypassType != CreatureObstaclesBypassType.None)
            {
                var obstaclesBypasser = obstaclesBypassersFactory.Create(_obstaclesBypassType, transform);
                _mover.AssignObstaclesBypasser(obstaclesBypasser);
            }
            
            if (_moveBoostType != CreatureMoveBoostType.None)
                _moveBooster = moveBoostersFactory.Create(_moveBoostType, _rigidbody2D, _statsCalculator);

            _destroyer = destroyersFactory.Create(_destroyType, this);

            _animationLauncher = animationLaunchersFactory.Create(
                _animatingType,
                _animator,
                _animatorOverrideController,
                _mover,
                _health);
            
            _animationLauncher.InitializeTriggerAnimations();
            
            _pauser = pauser;
        }
        
        public CreatureType CreatureType => _creatureType;
        public ICreatureHealth Health => _health;
        public ICreatureMoveBooster MoveBooster => _moveBooster;
        
        private void OnDisable()
        {
            _health.OnHealthGone -= Destroy;
            _animationLauncher.Dispose();
        }

        private void Update()
        {
            if (_pauser.IsGamePaused) return;

            // for testing
            if (Input.GetKeyDown(KeyCode.L))
            {
                TakeHit(new DamageInfo(1));
            }
            
            HandleSpriteFlip();
            
            if (_moveBoostType != CreatureMoveBoostType.None)
                _moveBooster.HandleTimings();
        }

        private void FixedUpdate()
        {
            if (_pauser.IsGamePaused) return;
            
            _mover.EnableMove();
            _animationLauncher.AnimateMoving();
            
            if (_moveBoostType != CreatureMoveBoostType.None)
                _moveBooster.EnableBoost();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_pauser.IsGamePaused) return;
            
            if (other.gameObject.TryGetComponent<Creature>(out var otherCreature))
                if (otherCreature._creatureType != _creatureType)
                    Destroy();
        }

        public override void OnTakenFromPool()
        {
            _health.Refresh();
        }
        
        public void TakeHit(DamageInfo damageInfo)
        {
            _health.ChangeBy(-damageInfo.DamageToDeal);
        }

        private void HandleSpriteFlip()
        {
            if (_rigidbody2D.velocity.x < 0 && !_isSpriteFlipped)
            {
                _spriteRenderer.flipX = true;
                _isSpriteFlipped = true;
            }
            else if (_rigidbody2D.velocity.x > 0 && _isSpriteFlipped)
            {
                _spriteRenderer.flipX = false;
                _isSpriteFlipped = false;
            }
        }
        
        private void Destroy()
        {
            _destroyer.DestroyCreature();
        }
    }
}