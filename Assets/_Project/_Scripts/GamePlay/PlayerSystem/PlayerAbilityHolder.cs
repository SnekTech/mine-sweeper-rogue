using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.AbilitySystem;
using UnityEngine;

namespace SnekTech.GamePlay.PlayerSystem
{
    [CreateAssetMenu(menuName = C.MenuName.Player + "/" + nameof(PlayerAbilityHolder))]
    public class PlayerAbilityHolder : ScriptableObject, IPlayerDataHolder
    {
        public event Action<List<PlayerAbility>> Changed;

        [SerializeField]
        private Player player;

        private static List<PlayerAbility> AllAbilities => PlayerAbilityRepo.Instance.Assets;

        private static List<PlayerAbility> ActiveAbilities =>
            AllAbilities.Where(ability => ability.IsActive).ToList();

        public async UniTask UseAbilities()
        {
            var tasks = ActiveAbilities.Select(ability => ability.Use(player));
            await UniTask.WhenAll(tasks);

            Changed?.Invoke(ActiveAbilities);
        }

        public void AddAbility(PlayerAbility playerAbility, int repeatTimes)
        {
            playerAbility.RepeatTimes += repeatTimes;
            Changed?.Invoke(ActiveAbilities);
        }

        public void RemoveAbility(PlayerAbility playerAbility)
        {
            playerAbility.RepeatTimes = 0;
            Changed?.Invoke(ActiveAbilities);
        }

        public void ClearAll()
        {
            AllAbilities.ForEach(ability => ability.RepeatTimes = 0);
            Changed?.Invoke(ActiveAbilities);
        }

        public void LoadData(PlayerData playerData)
        {
            var data = playerData.abilityData;
            foreach (var (abilityName, repeatTimes) in data.repeatTimesByAbilityName)
            {
                var ability = PlayerAbilityRepo.Instance.Get(abilityName);
                ability.RepeatTimes = repeatTimes;
            }
            Changed?.Invoke(ActiveAbilities);
        }

        public void SaveData(PlayerData playerData)
        {
            var data = playerData.abilityData;
            foreach (var ability in AllAbilities)
            {
                data.repeatTimesByAbilityName[ability.name] = ability.RepeatTimes;
            }
        }
    }
}
