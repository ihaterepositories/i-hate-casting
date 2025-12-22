using DG.Tweening;
using UIServices.ImageFadeAnimators.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UIServices.ImageFadeAnimators
{
    public class SmoothlyImageFadeAnimator : IImageFadeAnimator
    {
        private readonly Image _image;
        
        private readonly float _fadeInAlpha = 0.85f;
        private readonly float _fadeInDuration = 0.5f;
        private readonly float _fadeOutDuration = 0.25f;
        
        public SmoothlyImageFadeAnimator(Image image)
        {
            _image = image;
        }
        
        public void FadeIn()
        {
            _image
                .DOFade(_fadeInAlpha, _fadeInDuration)
                .SetUpdate(true);
        }

        public void FadeOut()
        {
            _image
                .DOFade(0f, _fadeOutDuration)
                .SetUpdate(true);
        }

        public void ForceCleanUp()
        {
            _image.DOKill();
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
        }
    }
}