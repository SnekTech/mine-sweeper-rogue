using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.PlayerDataAccumulator
{
    // An accumulator stays in PlayerState object as long as not removed,
    // affecting the calculation of total player data
    public interface IPlayerDataAccumulator
    {
        void Accumulate(PlayerStats playerStats);
    }
}