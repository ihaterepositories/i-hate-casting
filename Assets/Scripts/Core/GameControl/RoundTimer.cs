using System;
using TMPro;
using UnityEngine;
using Utils;

namespace Core.GameControl
{
    // TODO: Separate UI and Logic
    public class RoundTimer : MonoBehaviour
    { 
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private float _roundDurationInMinutes = 20f;
        
        private float _roundDurationInSeconds => _roundDurationInMinutes * 60f;
        private float _elapsedTime;

        private bool _isTimeExpired;
        
        public static event Action OnRoundTimeExpired;

        private void Awake()
        {
            _elapsedTime = _roundDurationInSeconds;
            UpdateTimerText();
        }

        private void Update()
        {
            if (GameStateHolder.IsGamePaused) return;
            if (_isTimeExpired) return;
            
            _elapsedTime -= Time.deltaTime;
            UpdateTimerText();
            
            if (_elapsedTime > 0) return;
            
            _isTimeExpired = true;
            OnRoundTimeExpired?.Invoke();
        }

        private void UpdateTimerText()
        {
            _timerText.text = TimeFormatter.Format(_elapsedTime);
        }
    }
}