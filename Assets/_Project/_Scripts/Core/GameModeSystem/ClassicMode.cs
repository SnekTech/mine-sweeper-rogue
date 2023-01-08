using SnekTech.Grid;
using SnekTech.Player;

namespace SnekTech.Core.GameModeSystem
{
    public class ClassicMode : GameMode
    {
        private readonly GridEventManager _gridEventManager;

        public ClassicMode(GridEventManager gridEventManager, GameModeInfo gameModeInfo, PlayerState playerState)
            : base(gameModeInfo, playerState)
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
            PlayerState.HealthRanOut += OnPlayerHealthRanOut;
        }

        private void OnPlayerHealthRanOut()
        {
            InvokeLevelCompleted(true);
        }

        protected override void OnStop()
        {
            _gridEventManager.OnGridClear -= HandleOnGridClear;
            PlayerState.HealthRanOut -= OnPlayerHealthRanOut;
        }
    }
}
