using Cysharp.Threading.Tasks;
using SnekTech.GridCell;

namespace SnekTech.GamePlay.WeaponSystem
{
    public interface IWeapon
    {
        UniTask Primary(ICell cell);
        UniTask Secondary(ICell cell);
    }
}