using Models.Creatures.Services.ObstaclesBypassing.Enums;

namespace Models.Creatures.Services.ObstaclesBypassing.Dtos
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
        
        public static ObstaclesBypassConfig GetFor(CreatureObstaclesBypassType type)
        {
            return type switch
            {
                CreatureObstaclesBypassType.None => new ObstaclesBypassConfig(0f, 0f),
                CreatureObstaclesBypassType.Light => new ObstaclesBypassConfig(1f, 1f),
                CreatureObstaclesBypassType.Heavy => new ObstaclesBypassConfig(2f, 2f),
                _ => new ObstaclesBypassConfig(0f, 0f)
            };
        }
    }
}