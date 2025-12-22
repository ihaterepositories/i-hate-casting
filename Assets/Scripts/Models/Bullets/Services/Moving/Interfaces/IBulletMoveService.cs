namespace Models.Bullets.Services.Moving.Interfaces
{
    public interface IBulletMoveService
    {
        /// <summary>
        /// Call it in Update loop to make bullet move.
        /// </summary>
        public void EnableMove();
        public void UpdateSpeed(float newSpeed);
    }
}