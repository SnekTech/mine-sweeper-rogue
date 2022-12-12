﻿namespace SnekTech.Player.PlayerDataAccumulator
{
    public class MaxHealthAccumulator : PlayerDataAccumulator
    {
        private readonly int _changeAmount;

        public MaxHealthAccumulator(int changeAmount, PlayerDataAccumulator decoratedAccumulator = null)
            : base(decoratedAccumulator)
        {
            _changeAmount = changeAmount;
        }

        protected override void DoAccumulate(PlayerData playerData)
        {
            // todo: add max-health related feature
            playerData.maxHealth += _changeAmount;
        }
    }
}