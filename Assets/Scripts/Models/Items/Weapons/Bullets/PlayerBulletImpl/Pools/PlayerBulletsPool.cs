using Models.WorldObjects.Base.Pooling;

namespace Models.Items.Weapons.Bullets.PlayerBulletImpl.Pools
{
    // Created specific pool container to bind it in the installer for weapon
    // to use it even after a game start (weapon don`t spawn at the start).
    public class PlayerBulletsPool : PoolContainer<PlayerBullet>
    {
        
    }
}