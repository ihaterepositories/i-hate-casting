using Systems.Combat.Dtos;

namespace Systems.Combat.Interfaces
{
    public interface IDamageable
    {
        public void TakeHit(DamageInfo damageInfo);
    }
}