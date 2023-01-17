using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.Editor.Animation
{
    public class AsepriteClipMetaData
    {
        public readonly string Name;
        public readonly List<float> FrameDurations;
        public readonly int StartIndex;

        public AsepriteClipMetaData(string name, List<float> frameDurations, int startIndex)
        {
            Name = name;
            FrameDurations = frameDurations;
            StartIndex = startIndex;
        }
    }
}