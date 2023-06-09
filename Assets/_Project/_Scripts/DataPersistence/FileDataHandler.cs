﻿using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SnekTech.DataPersistence
{
    public class FileDataHandler
    {
        private readonly string _dataDirPath;
        private readonly string _dataFileName;

        private string FullPath => Path.Combine(_dataDirPath, _dataFileName);

        public bool HasFileData => File.Exists(FullPath);

        public FileDataHandler(string dataDirPath, string dataFileName)
        {
            _dataDirPath = dataDirPath;
            _dataFileName = dataFileName;
        }

        public GameData Load()
        {
            GameData loadedData = null;
            if (File.Exists(FullPath))
            {
                try
                {
                    string dataToLoad;
                    using (var stream = new FileStream(FullPath, FileMode.Open))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error occured when trying to load data from file: {FullPath}\n" + e);
                }
            }

            return loadedData;
        }

        public void Save(GameData data)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FullPath)!);
                var dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented);

                using var stream = new FileStream(FullPath, FileMode.Create);
                using var writer = new StreamWriter(stream);
                writer.Write(dataToStore);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to save data to file: {FullPath}\n" + e);
            }
        }
    }
}
