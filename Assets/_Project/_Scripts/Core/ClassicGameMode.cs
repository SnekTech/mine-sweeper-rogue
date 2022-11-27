using SnekTech.Grid;

namespace SnekTech.Core
{
    public class ClassicGameMode : IGameMode
    {
        public bool HasLevelCleared => _hasLevelCleared;

        private bool _hasLevelCleared;
        private readonly GridEventManager _gridEventManager;

        public ClassicGameMode(GridEventManager gridEventManager)
        {
            _gridEventManager = gridEventManager;
            _hasLevelCleared = false;

            _gridEventManager.GridCleared += OnGridCleared;
        }

        private void OnGridCleared(IGrid grid)
        {
            _hasLevelCleared = true;
            _gridEventManager.GridCleared -= OnGridCleared;
        }
    }
}
