using Core.GameEventsControl.Signals.Interfaces;

namespace Core.GameEventsControl.Interfaces
{
    public interface IEventBusInvoker
    {
        /// <summary>
        /// Invokes Event bus signals.
        /// In other words, calls subscribed to the specified signals (throw IEventBusSubscriber) methods, when they are invoked. 
        /// </summary>
        /// <param name="signal">Signal instance.</param>
        /// <typeparam name="T">Type of signal.</typeparam>
        public void Invoke<T>(T signal) where T : IEventBusSignal;
    }
}