using Cysharp.Threading.Tasks;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;

namespace SnekTech.GamePlay.WeaponSystem
{
    public interface IWeapon
    {
        UniTask Primary(ICell cell);
        UniTask Secondary(ICell cell);
    }
}