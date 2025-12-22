namespace Systems.ResourcesCleaning.Interfaces
{
    public interface IResourceCleanable
    {
        /// <summary>
        /// Use this method right before object killing.
        /// </summary>
        public void CleanResources();
    }
}