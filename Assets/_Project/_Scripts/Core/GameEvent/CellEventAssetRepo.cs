using SnekTech.DataPersistence;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu(menuName = C.MenuName.DataPersistenceSystem + "/" + nameof(CellEventAssetRepo))]
    public class CellEventAssetRepo : AssetRepo<CellEvent>
    {
    }
}
