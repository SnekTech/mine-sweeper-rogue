using SnekTech.GamePlay;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.Grid;

namespace SnekTech.Core.GameModeSystem
{
    public class ClassicMode : GameMode
    {
        private readonly GridEventManager _gridEventManager;

        public ClassicMode(GridEventManager gridEventManager, GameModeInfo gameModeInfo, PlayerEventChannel playerEventChannel)
            : base(gameModeInfo, playerEventChannel)
        {
            _gridEventManager = gridEventManager;
        }

        private void HandleOnGridClear(IGrid grid)
        {
            InvokeLevelCompleted(false);
        }

        protected override void OnStart()
        {
            _gridEventManager.OnGridClear += HandleOnGridClear;
            PlayerEventChannel.HealthRanOut += OnPlayerHealthRanOut;
        }

        private void OnPlayerHealthRanOut()
        {
            InvokeLevelCompleted(true);
        }

        protected override void OnStop()
        {
            _gridEventManager.OnGridClear -= HandleOnGridClear;
            PlayerEventChannel.HealthRanOut -= OnPlayerHealthRanOut;
        }
    }
}
