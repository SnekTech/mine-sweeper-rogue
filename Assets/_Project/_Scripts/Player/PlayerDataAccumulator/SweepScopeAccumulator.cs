namespace SnekTech.Player.PlayerDataAccumulator
{
    public class SweepScopeAccumulator : PlayerDataAccumulator
    {
        private int _changeAmount;

        public SweepScopeAccumulator(int changeAmount, PlayerDataAccumulator decoratedAccumulator = null)
            : base(decoratedAccumulator)
        {
            _changeAmount = changeAmount;
        }

        protected override void DoAccumulate(PlayerData playerData)
        {
            playerData.sweepScope += _changeAmount;
        }
    }
}
