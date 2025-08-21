using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Core;
using Mechanics.Casting;
using TMPro;
using UnityEngine;
using UserInterface.Animators.Enums;
using UserInterface.Functional.Menus.Base;
using UserInterface.Functional.Menus.CastingMenuImplementation.Models;

namespace UserInterface.Functional.Menus.CastingMenuImplementation
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

        public void OpenCastingPuzzleThen(Action onSolvedCallback)
        {
            _onSolvedCallback = onSolvedCallback;
            
            OpenMenu(ScreenBorderType.CastingMenuBorder);
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
                // Callback delayed due to end the casting menu border hide animation
                StartCoroutine(InvokeOnSolvedCallbackDelayed());
            }
            else
            {
                Debug.Log("Casting failed. [Create a notify text for player]");
            }
        }

        private IEnumerator InvokeOnSolvedCallbackDelayed()
        {
            yield return new WaitForSecondsRealtime(AppConstants.ExtraScreenBorderAppearanceTime);
            _onSolvedCallback?.Invoke();
        }
    }
}