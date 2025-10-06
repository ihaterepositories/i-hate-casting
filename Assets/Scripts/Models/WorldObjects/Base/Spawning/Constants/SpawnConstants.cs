using UnityEngine;

namespace Models.WorldObjects.Base.Spawning.Constants
{
    public static class SpawnConstants
    {
        public static readonly Vector2 SpawnAreaSize = new Vector2(24f, 12f);
        public static readonly float MinDistanceFromPlayerWhenSpawn = Camera.main!.orthographicSize * 1.5f;
        public static readonly float MinDistanceFromOtherCollidersWhenSpawn = 3f;
    }
}