using System;
using Core.GameEventsControl.Signals.Interfaces;

namespace Core.GameEventsControl.Interfaces
{
    public interface IEventBusSubscriber
    {
        /// <summary>
        /// Subscribes method to a specified signals.
        /// The method will be called when the signal invoked (throw IEventBusInvoker).
        /// </summary>
        /// <param name="method">Method which must be subscribed.</param>
        /// <typeparam name="T">Type of signals to which the method must be subscribed.</typeparam>
        public void Subscribe<T>(Action<T> method) where T : IEventBusSignal;
        
        /// <summary>
        /// Unsubscribes method from a specified signals.
        /// Now the method will not be called when the signal invoked.
        /// </summary>
        /// <param name="method">Method which must be unsubscribed.</param>
        /// <typeparam name="T">Type of signals from which the method must be unsubscribed.</typeparam>
        public void Unsubscribe<T>(Action<T> method) where T : IEventBusSignal;
    }
}