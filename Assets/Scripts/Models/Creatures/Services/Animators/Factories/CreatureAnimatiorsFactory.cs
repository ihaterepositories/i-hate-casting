using System;
using Models.Creatures.Services.Animators.Enums;
using Models.Creatures.Services.Animators.Interfaces;
using Models.Creatures.Services.Health.Interfaces;
using Models.Creatures.Services.Movers.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Animators.Factories
{
    public class CreatureAnimatiorsFactory
    {
        public ICreatureAnimator Create(
            CreatureAnimatingType animatingType, 
            Animator animator, 
            AnimatorOverrideController overrideController,
            ICreatureMover moveService,
            ICreatureHealth healthService)
        {
            return animatingType switch
            {
                CreatureAnimatingType.Default => new CreatureAnimator(animator, overrideController, moveService, healthService),
                _ => throw new ArgumentOutOfRangeException(nameof(animatingType), animatingType, null)
            };
        }
    }
}