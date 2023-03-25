using SnekTech.GamePlay.PlayerSystem;
using SnekTech.GridSystem;
using SnekTech.MineSweeperRogue.GridSystem;

namespace SnekTech.Core.GameModeSystem
{
    public class ClassicMode : GameMode
    {
        private readonly GridEventChannel _gridEventChannel;

        public ClassicMode(GridEventChannel gridEventChannel, GameModeInfo gameModeInfo,
            PlayerEventChannel playerEventChannel)
            : base(gameModeInfo, playerEventChannel)
        {
            _gridEventChannel = gridEventChannel;
        }

        private void HandleOnGridClear(IGrid grid)
        {
            InvokeLevelCompleted(false);
        }

        protected override void OnStart()
        {
            _gridEventChannel.OnGridClear += HandleOnGridClear;
            PlayerEventChannel.HealthRanOut += OnPlayerHealthRanOut;
        }

        private void OnPlayerHealthRanOut()
        {
            InvokeLevelCompleted(true);
        }

        protected override void OnStop()
        {
            _gridEventChannel.OnGridClear -= HandleOnGridClear;
            PlayerEventChannel.HealthRanOut -= OnPlayerHealthRanOut;
        }
    }
}