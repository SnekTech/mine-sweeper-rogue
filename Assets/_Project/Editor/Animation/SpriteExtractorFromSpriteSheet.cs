using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SnekTech.Editor.Animation
{
    public class SpriteExtractorFromSpriteSheet
    {
        private readonly List<Sprite> sprites;

        public SpriteExtractorFromSpriteSheet(string spriteSheetPath)
        {
            var spriteArr = AssetDatabase.LoadAllAssetRepresentationsAtPath(spriteSheetPath);
            sprites = new List<Sprite>();
            foreach (var sprite in spriteArr!)
            {
                sprites.Add((Sprite)sprite);
            }
        }

        public List<Sprite> GetSpritesWithInRange(int start, int length)
        {
            return sprites.GetRange(start, length);
        }
    }
}
