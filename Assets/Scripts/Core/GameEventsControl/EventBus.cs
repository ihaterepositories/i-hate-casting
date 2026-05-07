using System;
using System.Collections.Generic;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals.Interfaces;
using UnityEngine;

namespace Core.GameEventsControl
{
    public class EventBus : IEventBusInvoker, IEventBusSubscriber
    {
        // Callbacks dictionary
        // key - signal's class name
        // value - list of methods, that should be called when signal invoked
        private Dictionary<string, List<object>> _callbacks = new();
        
        public void Invoke<T>(T signal) where T : IEventBusSignal
        {
            string key = typeof(T).ToString();

            if (_callbacks[key] != null && _callbacks[key].Count > 0)
            {
                // Creating new array to prevent Exception
                // (possible InvalidOperationException: Collection was modified).
                // Exception can be happened if previous called action unsubscribed next.
                var callbacks = _callbacks[key].ToArray();

                foreach (var obj in callbacks)
                {
                    var callback = obj as Action<object>;
                    callback?.Invoke(signal);
                }
            }
            else
            {
                Debug.LogWarning($"EventBus: {key} signal have no subscribers.");
            }
        }

        public void Subscribe<T>(Action<T> method) where T : IEventBusSignal
        {
            string key = typeof(T).ToString();
            
            // Registering new callbacks collection if not exists
            if (!_callbacks.TryGetValue(key, out var list))
            {
                list = new List<object>();
                _callbacks[key] = list;
            }
            
            _callbacks[key].Add(method);
        }

        public void Unsubscribe<T>(Action<T> method) where T : IEventBusSignal
        {
            string key = typeof(T).ToString();
            
            if (_callbacks.TryGetValue(key, out var list))
            {
                list.Remove(method);
            }
        }
    }
}