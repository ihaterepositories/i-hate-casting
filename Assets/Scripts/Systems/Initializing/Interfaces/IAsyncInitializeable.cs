using System.Collections;

namespace Systems.Initializing.Interfaces
{
    /// <summary>
    /// Needs initializing before use.
    /// </summary>
    public interface IAsyncInitializeable
    {
        public IEnumerator InitializeCoroutine();
    }
}