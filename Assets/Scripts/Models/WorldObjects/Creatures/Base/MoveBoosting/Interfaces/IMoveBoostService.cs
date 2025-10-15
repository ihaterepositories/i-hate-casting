namespace Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces
{
    public interface IMoveBoostService
    {
        public float BoostCooldownDuration { get; }
        public float BoostCooldownTimeElapsed { get; }
        public void ActivateBooster();
        public void HandleTimings();
    }
}