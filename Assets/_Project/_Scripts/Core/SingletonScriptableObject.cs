using System;
using UnityEngine;

namespace SnekTech.Core
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        private static T s_instance;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    var assets = Resources.LoadAll<T>("");
                    if (assets == null || assets.Length < 1)
                    {
                        throw new Exception($"Could not find any singleton SO of type {typeof(T).Name}.");
                    }
                    else if (assets.Length > 1)
                    {
                        Debug.LogWarning($"Multiple instances of singleton SO type {typeof(T).Name} found in the resources.");
                    }

                    s_instance = assets[0];
                }

                return s_instance;
            }
        }
    }
}
