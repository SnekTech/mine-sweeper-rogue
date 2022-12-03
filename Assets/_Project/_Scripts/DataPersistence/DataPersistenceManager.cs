using System;
using System.Collections.Generic;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")]
        [SerializeField]
        private string fileName;

        [Header("Scriptable Objects to Save")]
        [SerializeField]
        private PlayerState playerState;

        private GameData _gameData;
        private FileDataHandler _dataHandler;

        private List<IPersistentDataHolder> PersistentDataHolders => new List<IPersistentDataHolder>
        {
            playerState,
        };
        
        public DataPersistenceManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"{nameof(DataPersistenceManager)}.{nameof(Instance)} should be null on awake");
            }

            Instance = this;
        }

        private void Start()
        {
            _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void NewGame()
        {
            _gameData = new GameData();
        }

        public void LoadGame()
        {
            _gameData = _dataHandler.Load();
            
            if (_gameData == null)
            {
                NewGame();
            }

            foreach (IPersistentDataHolder dataHolder in PersistentDataHolders)
            {
                dataHolder.LoadData(_gameData);
            }
        }

        public void SaveGame()
        {
            foreach (IPersistentDataHolder dataHolder in PersistentDataHolders)
            {
                dataHolder.SaveData(_gameData);
            }
            
            _dataHandler.Save(_gameData);
        }
    }
}
