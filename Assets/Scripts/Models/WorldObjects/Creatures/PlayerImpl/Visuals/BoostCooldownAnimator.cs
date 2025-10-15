using System;
using System.Collections;
using Models.WorldObjects.Creatures.Base;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
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
            _boostService = player.MoveBoostService;
        }

        private void Update()
        {
            UpdateBar(_boostService.BoostCooldownTimeElapsed, _boostService.BoostCooldownDuration);
        }
    }
}