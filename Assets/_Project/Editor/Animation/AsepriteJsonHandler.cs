using System.Collections.Generic;
using Newtonsoft.Json;
using SnekTech.Core.Animation;
using UnityEngine;

namespace SnekTech.Editor.Animation
{
    public static class AsepriteJsonHandler
    {

        public static AsepriteJsonData ExtractClipMetaData(string jsonText)
        {
            return JsonConvert.DeserializeObject<AsepriteJsonData>(jsonText);
        }

        public static List<SnekAnimationClip> GetSnekClipsFromAsepriteJson(AsepriteJsonData asepriteJsonData, string spriteSheetPath)
        {
            var clipAssets = new List<SnekAnimationClip>();
            var frames = asepriteJsonData.Frames;

            foreach (var tag in asepriteJsonData.Meta.Tags)
            {
                var clipAsset = ScriptableObject.CreateInstance<SnekAnimationClip>();

                var frameDurations = new List<int>();
                for (int i = tag.FromIndex; i <= tag.ToIndex; i++)
                {
                    frameDurations.Add(frames[i].Duration);
                }

                clipAsset.AnimName = tag.Name;
                clipAsset.Sprites = SpriteExtractorFromSpriteSheet.GetSpritesFromSpriteSheet(spriteSheetPath)
                    .GetRange(tag.FromIndex, tag.Length);
                clipAsset.FrameDurations = frameDurations;
                clipAsset.IsLooping = tag.Name.ToLower().Contains("loop");
                
                clipAssets.Add(clipAsset);
            }

            return clipAssets;
        }
    }
}