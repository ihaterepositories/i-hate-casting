using Models.Creatures.Implementations.PlayerImplementation;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Implementations.EnemyImplementation.Movers
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Enemy _enemy;

        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        private void FixedUpdate()
        {
            ChangeVelocity();
        }

        private void ChangeVelocity()
        {
            _rb.velocity = CalculateDirectionToPlayerVector() * _enemy.StatsCalculator.GetSpeed();
        }

        private Vector2 CalculateDirectionToPlayerVector()
        {
            return (_player.transform.position - transform.position).normalized;
        }
    }
}