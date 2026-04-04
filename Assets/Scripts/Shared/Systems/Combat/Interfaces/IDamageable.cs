using Shared.Systems.Combat.Dtos;

namespace Shared.Systems.Combat.Interfaces
{
    public interface IDamageable
    {
        public void TakeHit(DamageInfo damageInfo);
    }
}