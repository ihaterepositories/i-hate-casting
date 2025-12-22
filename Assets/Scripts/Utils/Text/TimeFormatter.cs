using UnityEngine;

namespace Utils.Text
{
    public static class TimeFormatter
    {
        public static string Format(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
            return $"{minutes:00}:{seconds:00}";
        }
    }
}