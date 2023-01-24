using SnekTech.C;

namespace SnekTech.GamePlay.PlayerSystem
{
    public class PlayerStatsData
    {
        public int itemChoiceCount;
        public int sweepScope;
        public int damagePerBomb;
        public Life life;

        public PlayerStatsData()
        {
            life = Life.Default;
            sweepScope = GameConstants.SweepScopeMin;
            damagePerBomb = GameConstants.DamagePerBomb;
            itemChoiceCount = GameConstants.InitialItemChoiceCount;
        }

        public PlayerStatsData(PlayerStatsData other)
        {
            life = other.life;
            damagePerBomb = other.damagePerBomb;
            sweepScope = other.sweepScope;
            itemChoiceCount = other.itemChoiceCount;
        }
    }
}
