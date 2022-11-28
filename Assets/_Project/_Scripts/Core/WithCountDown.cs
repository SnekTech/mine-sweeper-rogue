using SnekTech.Player;

namespace SnekTech.Core
{
    public class WithCountDown : GameMode
    {
        private readonly Timer _timer;
        private readonly GameMode _decoratedMode;
        public WithCountDown(GameMode decoratedMode, Timer timer) : base(decoratedMode.PlayerData)
        {
            _decoratedMode = decoratedMode;
            _timer = timer;
        }

        protected override void OnStart()
        {
            _decoratedMode.Start();
            _decoratedMode.LevelCompleted += OnDecoratedModeLevelCompleted;
            
            _timer.StartCountDown(3); // todo: replace magic number
            _timer.TimedOut += OnTimedOut;
        }

        protected override void OnStop()
        {
            _decoratedMode.Stop();
            _decoratedMode.LevelCompleted -= OnDecoratedModeLevelCompleted;
            
            _timer.TimedOut -= OnTimedOut;
        }

        private void OnTimedOut()
        {
            InvokeLevelCompleted(true);
        }

        private void OnDecoratedModeLevelCompleted(bool hasFailed)
        {
            InvokeLevelCompleted(hasFailed);
        }
    }
}
