using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.MenuBased.Implementations.CastingMenuImpl
{
    public class CastingPuzzle : MonoBehaviour
    {
        [SerializeField] private float _castingTime = 20f;
        [SerializeField] private int _puzzleCharsCount = 8;
        
        private readonly List<string> _generatedChars = new();
        private float _timer;
        private bool _isRunning;
        private int _currentAnswerCheckIndex = 0;
        private Action _onSolvedCallback;
        
        public float Timer => _timer;
        public bool IsRunning => _isRunning;
        public int PuzzleCharsCount => _puzzleCharsCount;

        public event Action<List<string>> OnPuzzleGenerated;
        /// <summary>
        /// Invoked when a character is solved correctly.
        /// The parameter is the index of the character that was solved.
        /// </summary>
        public event Action<int> OnCharSolved; 
        public event Action<bool> OnPuzzleResult;
        
        private void Update()
        {
            if(!_isRunning) return;
            
            RunTimer();
            CheckUserAnswer();
        }
        
        public void Run()
        {
            GeneratePuzzle();
            _currentAnswerCheckIndex = 0;
            _timer = _castingTime;
            _isRunning = true;
        }

        private void GeneratePuzzle()
        {
            _generatedChars.Clear();
            for (var i = 0; i < _puzzleCharsCount; i++)
            {
                _generatedChars.Add(((char)Random.Range(65, 91)).ToString());
            }
            OnPuzzleGenerated?.Invoke(_generatedChars);
        }
        
        private void RunTimer()
        {
            _timer -= Time.unscaledDeltaTime;
            
            if (_timer <= 0f)
            {
                OnPuzzleResult?.Invoke(false);
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
                        OnCharSolved?.Invoke(_currentAnswerCheckIndex);
                        _currentAnswerCheckIndex++;

                        if (_currentAnswerCheckIndex >= _generatedChars.Count)
                        {
                            _isRunning = false;
                            OnPuzzleResult?.Invoke(true);
                        }
                    }
                    else
                    {
                        _isRunning = false;
                        OnPuzzleResult?.Invoke(false);
                    }

                    break;
                }
            }
        }
    }
}