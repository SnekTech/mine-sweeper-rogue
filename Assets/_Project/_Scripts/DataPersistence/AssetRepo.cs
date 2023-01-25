using System;
using System.Collections.Generic;
using System.Linq;
using SnekTech.Core;
using UnityEngine;

namespace SnekTech.DataPersistence
{
    public abstract class AssetRepo<T> : SingletonScriptableObject<AssetRepo<T>> where T : ScriptableObject
    {
        private readonly Dictionary<string, T> dict = new Dictionary<string, T>();

        private readonly string typeName = typeof(T).Name;

        public List<T> Assets => dict.Values.ToList();

        public void Set(string assetName, T asset)
        {
            if (dict.ContainsKey(assetName))
            {
                var assetStored = dict[assetName];
                if (asset != assetStored)
                {
                    throw new ArgumentException($"duplicate asset name in a repo of type {typeName}");
                }
            }

            dict[assetName] = asset;
        }

        public T Get(string assetName) => dict[assetName];
    }
}