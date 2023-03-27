using SnekTech.DataPersistence;
using UnityEngine;

namespace SnekTech.GamePlay.CellEventSystem
{
    [CreateAssetMenu(menuName = C.MenuName.DataPersistenceSystem + "/" + nameof(CellEventAssetRepo))]
    public class CellEventAssetRepo : AssetRepo<CellEvent>
    {
    }
}
