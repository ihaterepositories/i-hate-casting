using Models.Bullets.Services.LifeTimeCalculating.Enums;
using Models.Bullets.Services.Moving.Enums;
using Models.Creatures.Enums;
using UnityEngine;

namespace Models.Bullets.Dtos
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "ScriptableObjects/PrefabConfigs/BulletConfig")]
    public class BulletConfig : ScriptableObject, IConfig
    {
        [SerializeField] private BulletConfigKey _key;
        
        [Header("Behaviour settings")]
        [SerializeField] private CreatureType _bulletOwner;
        [SerializeField] private BulletMoveType _moveType;
        [SerializeField] private BulletLifeTimeCalculatorType _lifeTimeCalculatorType;

        [Header("View settings")]
        [SerializeField] private Sprite _sprite;
        
        public string Key => _key.ToString();
        public CreatureType BulletOwner => _bulletOwner;
        public BulletMoveType MoveType => _moveType;
        public BulletLifeTimeCalculatorType LifeTimeCalculatorType => _lifeTimeCalculatorType;
        public Sprite Sprite => _sprite;
    }
}