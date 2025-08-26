using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Menus.CastingMenuImplementation.Models
{
    public class CastingPuzzleCharView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _charText;
        [SerializeField] private ParticleSystem _solvedPuzzleCharEffect;
        [SerializeField] private Image _viewImage;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _solvedCharSprite;

        public void ShowChar(string character)
        {
            _charText.text = character;
        }
        
        public void ShowDefaultCharView()
        {
            _viewImage.sprite = _defaultSprite;
        }

        public void ShowSolvedCharView()
        {
            _solvedPuzzleCharEffect.Play();
            _charText.text = string.Empty;
            _viewImage.sprite = _solvedCharSprite;
        }
    }
}