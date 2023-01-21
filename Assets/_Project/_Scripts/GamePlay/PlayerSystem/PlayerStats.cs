using System;
using System.Collections.Generic;
using SnekTech.C;
using SnekTech.GamePlay.PlayerDataAccumulator;
using UnityEngine;

namespace SnekTech.GamePlay.PlayerSystem
{
    [Serializable]
    public class PlayerStats
    {
        public Life Life { get; private set; } = Life.Default;
        
        public int damagePerBomb;// todo: this should belong to the grid, as ability or effect target
        public int sweepScope; // todo: add weapon system and put this in it
        
        public int itemChoiceCount;
        public float eventChanceFactor = 1f;

        private readonly List<IPlayerDataAccumulator> _playerDataAccumulators = new List<IPlayerDataAccumulator>();

        public PlayerStats Calculated => CalculatePlayerData();

        public PlayerStats()
        {
            damagePerBomb = GameConstants.DamagePerBomb;
            sweepScope = GameConstants.SweepScopeMin;
            itemChoiceCount = GameConstants.InitialItemChoiceCount;
        }

        public PlayerStats(PlayerStats other)
        {
            damagePerBomb = other.damagePerBomb;
            sweepScope = other.sweepScope;
            itemChoiceCount = other.itemChoiceCount;
        }

        private PlayerStats CalculatePlayerData()
        {
            var statsCopy = new PlayerStats(this);
            foreach (var accumulator in _playerDataAccumulators)
            {
                accumulator.Accumulate(statsCopy);
            }

            statsCopy.sweepScope = Mathf.Clamp(statsCopy.sweepScope, GameConstants.SweepScopeMin, GameConstants.SweepScopeMax);

            return statsCopy;
        }

        public void AddDataAccumulator(IPlayerDataAccumulator accumulator)
        {
            _playerDataAccumulators.Add(accumulator);
            CalculatePlayerData();
        }

        public void RemoveDataAccumulator(IPlayerDataAccumulator accumulator)
        {
            _playerDataAccumulators.Remove(accumulator);
            CalculatePlayerData();
        }

    }
}
