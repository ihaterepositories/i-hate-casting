using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Mechanics.Casting;
using TMPro;
using UnityEngine;
using UserInterface.Menus.Base;
using UserInterface.Menus.CastingMenuImplementation.Models;

namespace UserInterface.Menus.CastingMenuImplementation
{
    public class CastingMenu : InGameMenu
    {
        [SerializeField] private CastingPuzzle _castingPuzzle;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private List<CastingPuzzleCharView> _castingPuzzleCharViews;
        
        private Action _onSolvedCallback;

        private void OnEnable()
        {
            _castingPuzzle.OnPuzzleGenerated += ShowPuzzle;
            _castingPuzzle.OnCharSolved += ShowSolvedChar;
            _castingPuzzle.OnPuzzleResult += HandleResult;
        }
        
        private void OnDisable()
        {
            _castingPuzzle.OnPuzzleGenerated -= ShowPuzzle;
            _castingPuzzle.OnCharSolved -= ShowSolvedChar;
            _castingPuzzle.OnPuzzleResult -= HandleResult;
        }
        
        private void Update()
        {
            if (_castingPuzzle.IsRunning)
            {
                _timerText.text = _castingPuzzle.Timer.ToString(CultureInfo.InvariantCulture);
            }
        }

        public void OpenCastingPuzzleThenIfSolved(Action onSolvedCallback)
        {
            _onSolvedCallback = onSolvedCallback;
            
            OpenMenu();
            
            // Delay the puzzle start to avoid issues
            // when the menu opening key is checked as a puzzle answer.
            StartCoroutine(RunPuzzleDelayedCoroutine());
        }
        
        private IEnumerator RunPuzzleDelayedCoroutine()
        {
            yield return new WaitForEndOfFrame();
            _castingPuzzle.Run();
        }

        private void ShowPuzzle(List<string> generatedPuzzle)
        {
            for (var i = 0; i < _castingPuzzle.PuzzleCharsCount; i++)
            {
                _castingPuzzleCharViews[i].ShowDefaultCharView();
                _castingPuzzleCharViews[i].ShowChar(generatedPuzzle[i]);
            }
        }

        private void ShowSolvedChar(int solvedCharIndex)
        {
            _castingPuzzleCharViews[solvedCharIndex].ShowSolvedCharView();
        }
        
        private void HandleResult(bool isSuccess)
        {
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