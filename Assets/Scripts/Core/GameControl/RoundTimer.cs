using System;
using Core.Pausing.Interfaces;
using TMPro;
using UnityEngine;
using Utils.Text;
using Zenject;

namespace Core.GameControl
{
    // TODO: Separate UI and logic. Move it all to the UI Models folder.
    public class RoundTimer : MonoBehaviour
    { 
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private float _roundDurationInMinutes = 20f;
        
        private float _roundDurationInSeconds => _roundDurationInMinutes * 60f;
        private float _elapsedTime;

        private bool _isTimeExpired;

        private IPauser _pauser;

        [Inject]
        private void Construct(IPauser pauser)
        {
            _pauser = pauser;
        }
        
        public static event Action OnRoundTimeExpired;

        private void Awake()
        {
            _elapsedTime = _roundDurationInSeconds;
            UpdateTimerText();
        }

        private void Update()
        {
            if (_pauser.IsGamePaused) return;
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