using System;
using System.Collections.Generic;
using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.AbilitySystem
{
    public class PlayerAbilityHolder
    {
        public event Action<List<IPlayerAbility>> Changed;

        public PlayerAbilityHolder(IPlayer player)
        {
            _player = player;
        }

        private readonly IPlayer _player;

        private readonly List<IPlayerAbility> _clickAbilities = new List<IPlayerAbility>();
        private readonly List<IPlayerAbility> _moveAbilities = new List<IPlayerAbility>();

        private List<IPlayerAbility> AllAbilities
        {
            get
            {
                var list = new List<IPlayerAbility>();
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
        }

        public void UseMoveClickAbilities()
        {
            foreach (var moveAbility in _moveAbilities)
            {
                moveAbility.Use(_player);
            }
        }

        public void AddClickAbility(IPlayerAbility playerAbility)
        {
            _clickAbilities.Add(playerAbility);
            Changed?.Invoke(AllAbilities);
        }

        public void RemoveClickAbility(IPlayerAbility playerAbility)
        {
            _clickAbilities.Remove(playerAbility);
            Changed?.Invoke(AllAbilities);
        }

        public void AddMoveAbility(IPlayerAbility playerAbility)
        {
            _moveAbilities.Add(playerAbility);
            Changed?.Invoke(AllAbilities);
        }

        public void RemoveMoveAbility(IPlayerAbility playerAbility)
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
