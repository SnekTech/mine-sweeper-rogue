using SnekTech.C;
using SnekTech.DataPersistence;
using UnityEngine;

namespace SnekTech.GamePlay.AbilitySystem
{
    [CreateAssetMenu(menuName = MenuName.DataPersistenceSystem + "/" + nameof(PlayerAbilityRepo))]
    public class PlayerAbilityRepo : AssetRepo<PlayerAbility>
    {
    }
}
