using Core.Pausing.Interfaces;
using Models.Creatures.Dtos;
using Models.Creatures.Enums;
using Models.Creatures.Services.Animators.Enums;
using Models.Creatures.Services.Animators.Factories;
using Models.Creatures.Services.Animators.Interfaces;
using Models.Creatures.Services.Destroyers.Enums;
using Models.Creatures.Services.Destroyers.Factories;
using Models.Creatures.Services.Destroyers.Interfaces;
using Models.Creatures.Services.Health.Enums;
using Models.Creatures.Services.Health.Factories;
using Models.Creatures.Services.Health.Interfaces;
using Models.Creatures.Services.MoveBoosters.Enums;
using Models.Creatures.Services.MoveBoosters.Factories;
using Models.Creatures.Services.MoveBoosters.Interfaces;
using Models.Creatures.Services.Movers.Enums;
using Models.Creatures.Services.Movers.Factories;
using Models.Creatures.Services.Movers.Interfaces;
using Models.Creatures.Services.StatsScalers.Interfaces;
using Models.Creatures.Services.StatsScalers.Providers;
using Shared.Models.PoolableMonoBehaviours;
using Shared.Services.ObstaclesBypassCalculators.Enums;
using Shared.Services.ObstaclesBypassCalculators.Factories;
using Shared.Services.SpriteFlippers;
using Shared.Services.SpriteFlippers.Interfaces;
using Shared.Systems.Combat.Dtos;
using Shared.Systems.Combat.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Models.Creatures
{
    public class Creature : PoolableMonoBehaviour, IDamageable
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _spriteAnimator;
        
        [Header("Behaviour settings")]
        [SerializeField] private CreatureType _creatureType;
        [SerializeField] private CreatureHealthType _healthType;
        [SerializeField] private CreatureMoveType _moveType;
        [FormerlySerializedAs("_obstaclesBypassType")] [SerializeField] private ObstaclesBypassStrength _obstaclesBypassStrength;
        [SerializeField] private CreatureMoveBoostType _moveBoostType;
        [SerializeField] private CreatureDestroyType _destroyType;
        [SerializeField] private CreatureAnimatingType _animatingType;
        
        [Header("Stats")]
        [SerializeField] private CreatureStats _stats;

        [Header("View settings")] 
        [SerializeField] private AnimatorOverrideController _animatorOverrideController;
        
        // Services
        private ICreatureStatsScaler _statsScaler;
        private ICreatureHealth _health;
        private ICreatureMover _mover;
        private ICreatureMoveBooster _moveBooster;
        private ICreatureDestroyer _destroyer;
        private ICreatureAnimator _creatureAnimator;
        private ISpriteFlipper _spriteFlipper;
        private IPauser _pauser;

        [Inject]
        private void Construct(
            CreatureStatsScalersProvider statsScalersProvider,
            CreatureHealthesFactory healthesFactory,
            CreatureMoversFactory moversFactory,
            ObstaclesBypassCalculatorsFactory obstaclesBypassCalculatorsFactory,
            CreatureMoveBoostersFactory moveBoostersFactory,
            CreatureDestroyersFactory destroyersFactory,
            CreatureAnimatiorsFactory animatiorsFactory,
            IPauser pauser)
        {
            _statsScaler = statsScalersProvider.GetFor(_creatureType);
            
            _health = healthesFactory.Create(_healthType, _stats, _statsScaler);
            _health.OnHealthGone += Destroy;
            
            _mover = moversFactory.Create(_moveType, _rigidbody2D, _stats, _statsScaler, transform);
            
            if (_obstaclesBypassStrength != ObstaclesBypassStrength.None)
            {
                var obstaclesBypasser = obstaclesBypassCalculatorsFactory.Create(_obstaclesBypassStrength, transform);
                _mover.AssignObstaclesBypasser(obstaclesBypasser);
            }
            
            if (_moveBoostType != CreatureMoveBoostType.None)
                _moveBooster = moveBoostersFactory.Create(_moveBoostType, _rigidbody2D, _stats, _statsScaler);

            _destroyer = destroyersFactory.Create(_destroyType, this);

            _creatureAnimator = animatiorsFactory.Create(
                _animatingType,
                _spriteAnimator,
                _animatorOverrideController,
                _mover,
                _health);

            _spriteFlipper = new RigidBodiedSpriteFlipper(_rigidbody2D, _spriteRenderer);
            
            _pauser = pauser;
        }
        
        public CreatureType CreatureType => _creatureType;
        public ICreatureHealth Health => _health;
        public ICreatureMoveBooster MoveBooster => _moveBooster;
        
        private void OnDisable()
        {
            _health.OnHealthGone -= Destroy;
            _creatureAnimator.CleanResources();
        }

        private void Update()
        {
            if (_pauser.IsGamePaused) return;

            // for testing
            if (Input.GetKeyDown(KeyCode.L))
            {
                TakeHit(new DamageInfo(1));
            }
            
            _spriteFlipper.Tick();
            _creatureAnimator.Tick();
            
            if (_moveBoostType != CreatureMoveBoostType.None)
                _moveBooster.Tick();
        }

        private void FixedUpdate()
        {
            if (_pauser.IsGamePaused) return;
            
            _mover.FixedTick();
            
            if (_moveBoostType != CreatureMoveBoostType.None)
                _moveBooster.FixedTick();
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
        
        private void Destroy()
        {
            _destroyer.DestroyCreature();
        }
    }
}