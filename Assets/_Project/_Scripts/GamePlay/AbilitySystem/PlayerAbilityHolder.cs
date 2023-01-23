using System;
using System.Collections.Generic;
using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.AbilitySystem
{
    public class PlayerAbilityHolder
    {
        public event Action<List<PlayerAbility>> Changed;

        public PlayerAbilityHolder(IPlayer player)
        {
            _player = player;
        }

        private readonly IPlayer _player;

        private readonly List<PlayerAbility> _clickAbilities = new List<PlayerAbility>();
        private readonly List<PlayerAbility> _moveAbilities = new List<PlayerAbility>();

        private List<PlayerAbility> AllAbilities
        {
            get
            {
                var list = new List<PlayerAbility>();
                list.AddRange(_clickAbilities);
                list.AddRange(_moveAbilities);
                return list;
            }
        }

        public void UseClickAbilities()
        {
            foreach (var clickAbility in _clickAbilities)
            {
                clickAbility.Use(_player);
            }

            ClearInactiveAbilities();
        }

        private void ClearInactiveAbilities()
        {
            if (_clickAbilities.Count == 0) return;
            
            _clickAbilities.RemoveAll(ability => !ability.IsActive);
            Changed?.Invoke(AllAbilities);
        }

        public void UseMoveAbilities()
        {
            foreach (var moveAbility in _moveAbilities)
            {
                moveAbility.Use(_player);
            }
        }

        public void AddClickAbility(PlayerAbility playerAbility)
        {
            _clickAbilities.Add(playerAbility);
            Changed?.Invoke(AllAbilities);
        }

        public void RemoveClickAbility(PlayerAbility playerAbility)
        {
            _clickAbilities.Remove(playerAbility);
            Changed?.Invoke(AllAbilities);
        }

        public void AddMoveAbility(PlayerAbility playerAbility)
        {
            _moveAbilities.Add(playerAbility);
            Changed?.Invoke(AllAbilities);
        }

        public void RemoveMoveAbility(PlayerAbility playerAbility)
        {
            _moveAbilities.Remove(playerAbility);
            Changed?.Invoke(AllAbilities);
        }

        public void ClearAll()
        {
            _clickAbilities.Clear();
            _moveAbilities.Clear();
        }
    }
}
