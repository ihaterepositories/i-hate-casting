using System;
using UIServices.ImageFadeAnimators.Enums;
using UIServices.ImageFadeAnimators.Interfaces;
using UnityEngine.UI;

namespace UIServices.ImageFadeAnimators.Factories
{
    public class ImageFadeAnimatorsFactory
    {
        public IImageFadeAnimator Create(ImageFadingType imageFadingType, Image image)
        {
            return imageFadingType switch
            {
                ImageFadingType.Smooth => new SmoothlyImageFadeAnimator(image),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}