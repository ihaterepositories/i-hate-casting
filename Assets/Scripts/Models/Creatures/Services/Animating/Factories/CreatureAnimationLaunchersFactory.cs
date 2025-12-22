using System;
using Models.Creatures.Services.Animating.Enums;
using Models.Creatures.Services.Animating.Interfaces;
using Models.Creatures.Services.Living.Interfaces;
using Models.Creatures.Services.Moving.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Animating.Factories
{
    public class CreatureAnimationLaunchersFactory
    {
        public ICreatureAnimationLauncher Create(
            CreatureAnimatingType animatingType, 
            Animator animator, 
            AnimatorOverrideController overrideController,
            ICreatureMover moveService,
            ICreatureHealth healthService)
        {
            return animatingType switch
            {
                CreatureAnimatingType.Default => new CreatureAnimationLauncher(animator, overrideController, moveService, healthService),
                _ => throw new ArgumentOutOfRangeException(nameof(animatingType), animatingType, null)
            };
        }
    }
}