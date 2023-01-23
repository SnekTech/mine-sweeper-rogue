using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SnekTech.Editor
{
    public static class AssetFileUtils
    {
        public static string GetParentFolder(this Object asset)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            return GetParentFolder(assetPath);
        }
        
        public static string GetParentFolder(string assetPath)
        {
            int lastSlashIndex = assetPath.LastIndexOf("/", StringComparison.Ordinal);
            // exclude the last '/' character
            return assetPath[..lastSlashIndex];
        }

        public static ScriptableObject CreateAndSaveAsset(Type type, string assetPath, bool shouldOverwrite)
        {
            if (shouldOverwrite)
            {
                AssetDatabase.DeleteAsset(assetPath);
            }
            
            var asset = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(asset, assetPath);
            return asset;
        }

        public static ScriptableObject CreateAndSaveAsset<T>(string assetPath, bool shouldOverwrite) where T : ScriptableObject
        {
            return CreateAndSaveAsset(typeof(T), assetPath, shouldOverwrite);
        }
    }
}
