using SnekTech.Core.GameModeSystem;
using SnekTech.Grid;

namespace SnekTech.Core
{
    public class Level
    {
        private readonly GridData _gridData;
        private readonly GameMode _gameMode;
        private readonly int _index;

        public Level(GridData gridData, GameMode gameMode, int index)
        {
            _gridData = gridData;
            _gameMode = gameMode;
            _index = index;
        }

        public GridData GridData => _gridData;
        public GameMode GameMode => _gameMode;
        public int Index => _index;
    }
}
