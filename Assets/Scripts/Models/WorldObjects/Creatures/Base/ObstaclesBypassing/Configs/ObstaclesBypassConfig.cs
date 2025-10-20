using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Enums;

namespace Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Configs
{
    public class ObstaclesBypassConfig
    {
        public float DetectionRadius { get; private set; }
        public float BypassStrength { get; private set; }
        public float DistanceToPlayerWhenStopBypassing => 0.7f;
        
        public ObstaclesBypassConfig(float detectionRadius, float bypassStrength)
        {
            DetectionRadius = detectionRadius;
            BypassStrength = bypassStrength;
        }
        
        public static ObstaclesBypassConfig GetFor(ObstaclesBypassType type)
        {
            return type switch
            {
                ObstaclesBypassType.None => new ObstaclesBypassConfig(0f, 0f),
                ObstaclesBypassType.Light => new ObstaclesBypassConfig(1f, 1f),
                ObstaclesBypassType.Heavy => new ObstaclesBypassConfig(2f, 2f),
                _ => new ObstaclesBypassConfig(0f, 0f)
            };
        }
    }
}