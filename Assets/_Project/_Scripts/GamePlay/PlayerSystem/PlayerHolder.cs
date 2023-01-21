using SnekTech.C;
using SnekTech.DataPersistence;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.GamePlay.PlayerSystem
{
    [CreateAssetMenu(menuName = MenuName.Player + "/" + nameof(PlayerHolder))]
    public class PlayerHolder : ScriptableObject, IPersistentDataHolder
    {
        [SerializeField]
        private PlayerEventChannel playerEventChannel;
        
        [SerializeField]
        private GridEventManager gridEventManager;

        private Player _player;

        public Player Player => _player;

        private void OnDisable()
        {
            _player?.OnDisable();
        }

        public void LoadData(GameData gameData)
        {
            _player = gameData.player;
            _player.OnEnable(playerEventChannel, gridEventManager);
        }

        public void SaveData(GameData gameData)
        {
            gameData.player = _player;
        }
    }
}
