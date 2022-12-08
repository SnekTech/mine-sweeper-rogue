using SnekTech.Grid;
using SnekTech.Player;

namespace SnekTech.Core
{
    public class ClassicMode : GameMode
    {
        private readonly GridEventManager _gridEventManager;

        public ClassicMode(GridEventManager gridEventManager, PlayerState playerState)
            : base(playerState)
        {
            _gridEventManager = gridEventManager;
        }

        private void OnGridCleared(IGrid grid)
        {
            InvokeLevelCompleted(false);
        }

        protected override void OnStart()
        {
            _gridEventManager.GridCleared += OnGridCleared;
            PlayerState.HealthRanOut += OnPlayerHealthRanOut;
        }

        private void OnPlayerHealthRanOut()
        {
            InvokeLevelCompleted(true);
        }

        protected override void OnStop()
        {
            _gridEventManager.GridCleared -= OnGridCleared;
            PlayerState.HealthRanOut -= OnPlayerHealthRanOut;
        }
    }
}
