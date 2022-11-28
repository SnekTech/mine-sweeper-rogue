using System;
using SnekTech.Player;

namespace SnekTech.Core
{
    public abstract class GameMode
    {
        public event Action<bool> LevelCompleted;

        public readonly PlayerData PlayerData;

        protected GameMode(PlayerData playerData)
        {
            PlayerData = playerData;
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
            LevelCompleted = null;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();

        protected void InvokeLevelCompleted(bool hasFailed)
        {
            LevelCompleted?.Invoke(hasFailed);
        }
    }
}
