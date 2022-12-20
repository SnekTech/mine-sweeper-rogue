using SnekTech.Grid;

namespace SnekTech.Core
{
    public class Level
    {
        private readonly GridData _gridData;
        private readonly GameMode _gameMode;

        public Level(GridData gridData, GameMode gameMode)
        {
            _gridData = gridData;
            _gameMode = gameMode;
        }

        public GridData GridData => _gridData;
        public GameMode GameMode => _gameMode;
    }
}
