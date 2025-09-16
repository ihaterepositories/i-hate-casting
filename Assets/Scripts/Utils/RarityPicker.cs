using Models.Creatures.Items.Base.Enums;
using UnityEngine;

namespace Utils
{
    public static class RarityPicker
    {
        public static ItemRarity Generate()
        {
            int roll = Random.Range(0, 100); // 0–99

            if (roll < 70) return ItemRarity.Common;      // 70%
            else if (roll < 95) return ItemRarity.Gold;  // 25%
            else return ItemRarity.Incredible;           // 5%
        }
    }
}