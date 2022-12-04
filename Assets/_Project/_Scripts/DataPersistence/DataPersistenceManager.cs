using System;
using System.Collections.Generic;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.DataPersistence
{
    [CreateAssetMenu(menuName = nameof(DataPersistenceManager))]
    public class DataPersistenceManager : ScriptableObject
    {
        [Header("File Storage Config")]
        [SerializeField]
        private string fileName;

        [Header("Scriptable Objects to Save")]
        [SerializeField]
        private PlayerState playerState;

        private GameData _gameData;
        private FileDataHandler _fileDataHandler;

        public bool HasSavedGameData => _fileDataHandler.HasFileData;

        private List<IPersistentDataHolder> PersistentDataHolders => new List<IPersistentDataHolder>
        {
            playerState,
        };

        private void OnEnable()
        {
            _fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        }

        public void NewGame()
        {
            _gameData = new GameData();
            FulfillDataHolders(_gameData);
            SaveGame();
        }

        public void LoadGame()
        {
            _gameData = _fileDataHandler.Load();
            
            if (_gameData == null)
            {
                Debug.Log("no saved data found, return");
                return;
            }

            FulfillDataHolders(_gameData);
        }

        public void SaveGame()
        {
            CollectGameData(_gameData);
            
            _fileDataHandler.Save(_gameData);
        }

        private void FulfillDataHolders(GameData gameData)
        {
            foreach (IPersistentDataHolder dataHolder in PersistentDataHolders)
            {
                dataHolder.LoadData(gameData);
            }
        }

        private void CollectGameData(GameData gameData)
        {
            foreach (IPersistentDataHolder dataHolder in PersistentDataHolders)
            {
                dataHolder.SaveData(gameData);
            }
        }
    }
}
