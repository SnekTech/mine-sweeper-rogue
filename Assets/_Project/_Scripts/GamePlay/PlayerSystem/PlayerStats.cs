using System;
using System.Collections.Generic;
using SnekTech.C;
using SnekTech.GamePlay.PlayerDataAccumulator;
using UnityEngine;

namespace SnekTech.GamePlay.PlayerSystem
{
    [CreateAssetMenu(menuName = C.MenuName.Player + "/" + nameof(PlayerStats))]
    public class PlayerStats : ScriptableObject, IPlayerDataHolder
    {
        public Life Life { get; private set; } = Life.Default;
        
        public int damagePerBomb; // todo: this should belong to the grid, as ability or effect target
        public int sweepScope; // todo: add weapon system and put this in it
        
        public int itemChoiceCount;

        private PlayerStatsData baseData;

        private readonly List<IPlayerStatsDataAccumulator> _playerDataAccumulators = new List<IPlayerStatsDataAccumulator>();

        public PlayerStatsData Calculated => CalculatePlayerStats();


        private PlayerStatsData CalculatePlayerStats()
        {
            var statsCopy = new PlayerStatsData(baseData);
            foreach (var accumulator in _playerDataAccumulators)
            {
                accumulator.Accumulate(statsCopy);
            }

            statsCopy.sweepScope = Mathf.Clamp(statsCopy.sweepScope, GameConstants.SweepScopeMin, GameConstants.SweepScopeMax);

            return statsCopy;
        }

        public void AddDataAccumulator(IPlayerStatsDataAccumulator accumulator)
        {
            _playerDataAccumulators.Add(accumulator);
            CalculatePlayerStats();
        }

        public void RemoveDataAccumulator(IPlayerStatsDataAccumulator accumulator)
        {
            _playerDataAccumulators.Remove(accumulator);
            CalculatePlayerStats();
        }

        public void LoadData(PlayerData playerData)
        {
            var statsData = playerData.playerStatsData;
            baseData = statsData;
        }

        public void SaveData(PlayerData playerData)
        {
            playerData.playerStatsData = baseData;
        }
    }
}
