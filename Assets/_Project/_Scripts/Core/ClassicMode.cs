using SnekTech.Grid;
using SnekTech.Player;

namespace SnekTech.Core
{
    public class ClassicMode : GameMode
    {
        private readonly GridEventManager _gridEventManager;

        public ClassicMode(GridEventManager gridEventManager, PlayerData playerData)
            : base(playerData)
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
            PlayerData.HealthArmour.HealthRanOut += OnPlayerHealthRanOut;
        }

        private void OnPlayerHealthRanOut()
        {
            InvokeLevelCompleted(true);
        }

        protected override void OnStop()
        {
            _gridEventManager.GridCleared -= OnGridCleared;
            PlayerData.HealthArmour.HealthRanOut += OnPlayerHealthRanOut;
        }
    }
}
