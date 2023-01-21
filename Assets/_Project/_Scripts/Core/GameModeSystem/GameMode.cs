using System;
using SnekTech.GamePlay;
using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.Core.GameModeSystem
{
    public abstract class GameMode
    {
        public event Action<bool> OnLevelComplete;

        private readonly GameModeInfo _gameModeInfo;
        private readonly PlayerEventChannel _playerEventChannel;

        public GameModeInfo Info => _gameModeInfo;
        public PlayerEventChannel PlayerEventChannel => _playerEventChannel;

        protected GameMode(GameModeInfo gameModeInfo, PlayerEventChannel playerEventChannel)
        {
            _gameModeInfo = gameModeInfo;
            _playerEventChannel = playerEventChannel;
        }

        public void Start()
        {
            ClearEventSubscriptions();
            OnStart();
        }

        public void Stop()
        {
            OnStop();
        }

        private void ClearEventSubscriptions()
        {
            OnLevelComplete = null;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();

        protected void InvokeLevelCompleted(bool hasFailed) => OnLevelComplete?.Invoke(hasFailed);
    }
}
