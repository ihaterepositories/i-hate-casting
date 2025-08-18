using TMPro;
using UnityEngine;

namespace Mechanics.MenuBased.Casting.Models
{
    public class CastingKey : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _charText;
        private string _generatedChar;
        
        public string GeneratedChar => _generatedChar;
        
        public void GenerateChar()
        {
            _generatedChar = ((char)Random.Range(65, 91)).ToString(); // Generates a random uppercase letter A-Z
        }

        public void ShowChar()
        {
            _charText.text = _generatedChar;
        }
    }
}