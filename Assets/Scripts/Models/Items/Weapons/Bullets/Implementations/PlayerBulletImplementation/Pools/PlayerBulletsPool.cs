using Models.Pooling;

namespace Models.Items.Weapons.Bullets.Implementations.PlayerBulletImplementation.Pools
{
    // Created specific pool container to bind it in the installer for weapon
    // to use it even after a game start (weapon don`t spawn at the start).
    public class PlayerBulletsPool : PoolContainer<PlayerBullet>
    {
        
    }
}