using System;
using SnekTech.Player;

namespace SnekTech.Core.GameModeSystem
{
    public abstract class GameMode
    {
        public event Action<bool> OnLevelComplete;

        private readonly GameModeInfo _gameModeInfo;
        private readonly PlayerState _playerState;

        public GameModeInfo Info => _gameModeInfo;
        public PlayerState PlayerState => _playerState;

        protected GameMode(GameModeInfo gameModeInfo, PlayerState playerState)
        {
            _gameModeInfo = gameModeInfo;
            _playerState = playerState;
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
