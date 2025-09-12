using System;
using System.Collections;
using Models.Creatures.Base;
using Models.Creatures.Implementations.PlayerImplementation.Movers;
using UnityEngine;
using UnityEngine.UI;

namespace Models.Creatures.Implementations.PlayerImplementation.Visuals
{
    public class BurstCooldownAnimator : MonoBehaviour
    {
        [SerializeField] private Image _burstCooldownImage;
        [SerializeField] private Creature _creature;
        [SerializeField] private PlayerMover _mover;
        
        private float _burstCooldownTime => _creature.StatsCalculator.GetBurstCooldownTime();
        private Color _defaultColor;
        
        private void Awake()
        {
            _defaultColor = _burstCooldownImage.color;
        }

        private void OnEnable()
        {
            _mover.OnBurstActivated += StartBurstCooldownAnimation;
        }
        
        private void OnDisable()
        {
            _mover.OnBurstActivated -= StartBurstCooldownAnimation;
            
            StopAllCoroutines();
            _burstCooldownImage.fillAmount = 0;
        }

        private void StartBurstCooldownAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(BurstCooldownCoroutine());
        }
        
        private IEnumerator BurstCooldownCoroutine()
        {
            _defaultColor.a = 1f;
            _burstCooldownImage.color = _defaultColor;
            _burstCooldownImage.fillAmount = 1;
            
            float elapsedTime = 0f;
            while (elapsedTime < _burstCooldownTime)
            {
                elapsedTime += Time.deltaTime;
                _burstCooldownImage.fillAmount = 1 - Mathf.Clamp01(elapsedTime / _burstCooldownTime);
                yield return null;
            }
            
            _defaultColor.a = 0f;
            _burstCooldownImage.color = _defaultColor;
        }
    }
}