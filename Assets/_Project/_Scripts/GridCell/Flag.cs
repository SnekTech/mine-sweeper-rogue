using System;

namespace SnekTech.GridCell
{
    public class Flag
    {
        public event Action Disappeared;
        
        private FlagBehaviour _flagBehaviour;

        public Flag(FlagBehaviour flagBehaviour)
        {
            _flagBehaviour = flagBehaviour;
        }

        public void OnDisappeared()
        {
            Disappeared?.Invoke();
        }
    }
}
