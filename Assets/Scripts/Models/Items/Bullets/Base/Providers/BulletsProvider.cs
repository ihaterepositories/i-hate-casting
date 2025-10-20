using Models.Items.Bullets.Base.Enums;
using Models.Items.Bullets.EnemyBulletImpl.Pools;
using Models.Items.Bullets.PlayerBulletImpl.Pools;

namespace Models.Items.Bullets.Base.Providers
{
    public class BulletsProvider
    {
        private readonly PlayerBulletsPool _playerBulletsPool;
        private readonly EnemyBulletsPool _enemyBulletsPool;
        
        public BulletsProvider(PlayerBulletsPool playerBulletsPool, EnemyBulletsPool enemyBulletsPool)
        {
            _playerBulletsPool = playerBulletsPool;
            _enemyBulletsPool = enemyBulletsPool;
        }
        
        public Bullet Create(BulletType bulletType)
        {
            return bulletType switch
            {
                BulletType.PlayerBullet => _playerBulletsPool.GetFreeObject(),
                BulletType.EnemyBullet => _enemyBulletsPool.GetFreeObject(),
                _ => null
            };
        }
    }
}