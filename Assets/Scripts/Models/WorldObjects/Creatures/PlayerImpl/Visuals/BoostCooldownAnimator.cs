using Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces;
using UserInterface.StatusBar;
using Zenject;

namespace Models.WorldObjects.Creatures.PlayerImpl.Visuals
{
    public class BoostCooldownAnimator : StatusBarAnimator
    {
        private IMoveBoostService _boostService;

        [Inject]
        private void Construct(Player player)
        {
            _boostService = player.MoveBooster;
        }

        private void Update()
        {
            if (_boostService == null) return;
            
            UpdateBar(_boostService.BoostCooldownTimeElapsed, _boostService.BoostCooldownDuration);
        }
    }
}