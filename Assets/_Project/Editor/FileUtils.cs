using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SnekTech.Editor
{
    public static class FileUtils
    {
        public static string FormatToUnityPath(string path) => path.Replace("\\", "/");

        public static string FullPathToAssetPath(string fullPath) =>
            FormatToUnityPath(fullPath).Replace(Application.dataPath, "Assets");

        public static string GetFileName(string filePath, bool withoutExtension = true) =>
            FormatToUnityPath(
                withoutExtension ? Path.GetFileNameWithoutExtension(filePath) : Path.GetFileName(filePath));

        public static FileInfo[] GetFiles(string dirPath, string searchPattern = "*",
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (Directory.Exists(dirPath))
            {
                var dirInfo = new DirectoryInfo(dirPath);
                return dirInfo.GetFiles(searchPattern, searchOption);
            }

            UnityEngine.Debug.LogError("directory does not exist");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirPath">directory full path</param>
        /// <param name="searchPattern"></param>
        /// <param name="searchOption"></param>
        /// <returns>path relative to Assets directory</returns>
        public static List<string> GetFileAssetPaths(string dirPath, string searchPattern = "*",
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            var paths = new List<string>();
            var files = GetFiles(dirPath, searchPattern, searchOption);
            if (files is {Length: > 0})
            {
                paths.AddRange(files.Select(file => FullPathToAssetPath(file.FullName)));
            }

            return paths;
        }

        public static string GetAssetParentFolder(string assetPath)
        {
            int lastSlashIndex = assetPath.LastIndexOf("/", StringComparison.Ordinal);
            // exclude the last '/' character
            return assetPath[..lastSlashIndex];
        }

        public static string AssetPathCombine(string parent, string folderName) => parent + "/" + folderName;
    }
}
