using System;
using System.Collections;
using System.Collections.Generic;
using Mechanics.MenuBased.Base;
using Mechanics.MenuBased.Casting.Models;
using TMPro;
using UnityEngine;
using UserInterfaceUtils.Animators.Enums;

namespace Mechanics.MenuBased.Casting
{
    public class CastingMenu : InGameMenu
    {
        [SerializeField] private float _castingTime = 20f;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _hintText;
        [SerializeField] private List<CastingKey> _castingKeys;
        
        private List<string> _generatedChars = new();
        private float _timer;
        private bool _isCasting;
        private int _currentAnswerCheckIndex = 0;
        private Action _onSolvedCallback;

        private void Update()
        {
            if(!_isCasting) return;
            
            RunTimer();
            CheckUserAnswer();
        }

        public void CreateCastingPuzzleThen(Action onSolvedCallback)
        {
            GenerateChars();
            _onSolvedCallback = onSolvedCallback;
            
            OpenMenu(ScreenBorderType.CastingMenuBorder);
            StartCoroutine(RunCastingPuzzleCoroutine(0.5f));
        }

        private IEnumerator RunCastingPuzzleCoroutine(float startDelay)
        {
            yield return new WaitForSecondsRealtime(startDelay);
            _currentAnswerCheckIndex = 0;
            _timer = _castingTime;
            _isCasting = true;
        }

        private void GenerateChars()
        {
            _generatedChars.Clear();
            foreach (var castingKey in _castingKeys)
            {
                castingKey.GenerateChar();
                castingKey.ShowChar();
                _generatedChars.Add(castingKey.GeneratedChar);
            }
        }
        
        private void RunTimer()
        {
            _timer -= Time.unscaledDeltaTime;
            _timerText.text = _timer.ToString("0.00");
            
            if (_timer <= 0f)
            {
                HandleResult(false);
            }
        }

        private void CheckUserAnswer()
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key) && key.ToString().Length == 1) // лише одна буква
                {
                    var pressedLetter = key.ToString()[0].ToString();

                    if (string.Equals(pressedLetter, _generatedChars[_currentAnswerCheckIndex], StringComparison.CurrentCultureIgnoreCase))
                    {
                        _currentAnswerCheckIndex++;

                        if (_currentAnswerCheckIndex >= _generatedChars.Count)
                        {
                            HandleResult(true);
                        }
                    }
                    else
                    {
                        HandleResult(false);
                    }

                    break; // щоб за один кадр не перевіряти зайві клавіші
                }
            }
        }
        
        private void HandleResult(bool isSuccess)
        {
            _isCasting = false;
            CloseMenu(!isSuccess);
            
            if (isSuccess)
            {
                _onSolvedCallback?.Invoke();
            }
            else
            {
                Debug.Log("Casting failed. [Create a notify text for player]");
            }
        }
    }
}