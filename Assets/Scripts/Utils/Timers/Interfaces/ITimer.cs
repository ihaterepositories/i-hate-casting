using System;

namespace Utils.Timers.Interfaces
{
    public interface ITimer
    {
        public float TimeLeftToExpire { get; }
        public event Action OnTimeExpired;
        public void Start(float time);
        public void Stop();
    }
}