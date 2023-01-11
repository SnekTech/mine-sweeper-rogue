using SnekTech.Core.GameEvent;
using UnityEditor;

namespace SnekTech.Editor.Pools
{
    [CustomEditor(typeof(CellEventPool))]
    public class CellEventDataPoolEditor : RandomPoolEditor<CellEventPool, CellEventData>
    {
    }
}