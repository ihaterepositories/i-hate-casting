namespace UIServices.ImageFadeAnimators.Interfaces
{
    public interface IImageFadeAnimator
    {
        public void FadeIn();
        public void FadeOut();
        
        /// <summary>
        /// Immediately clears visuals.
        /// </summary>
        public void ForceCleanUp();
    }
}