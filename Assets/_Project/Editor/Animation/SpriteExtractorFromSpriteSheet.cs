using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SnekTech.Editor.Animation
{
    public static class SpriteExtractorFromSpriteSheet
    {
        public static List<Sprite> GetSpritesFromSpriteSheet(string spriteSheetPath)
        {
            var spriteArr = AssetDatabase.LoadAllAssetRepresentationsAtPath(spriteSheetPath);

            return spriteArr.Cast<Sprite>().ToList();
        }
    }
}
