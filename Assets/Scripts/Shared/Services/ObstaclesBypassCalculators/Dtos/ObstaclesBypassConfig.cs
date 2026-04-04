using Shared.Services.ObstaclesBypassCalculators.Enums;

namespace Shared.Services.ObstaclesBypassCalculators.Dtos
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
        
        public static ObstaclesBypassConfig GetFor(ObstaclesBypassStrength strength)
        {
            return strength switch
            {
                ObstaclesBypassStrength.None => new ObstaclesBypassConfig(0f, 0f),
                ObstaclesBypassStrength.Light => new ObstaclesBypassConfig(1f, 1f),
                ObstaclesBypassStrength.Heavy => new ObstaclesBypassConfig(2f, 2f),
                _ => new ObstaclesBypassConfig(0f, 0f)
            };
        }
    }
}