using System.Collections.Generic;

namespace SnekTech.Editor.Animation
{
    public class AsepriteClipMetaData
    {
        public readonly string Name;
        public readonly List<float> FrameDurations;

        public AsepriteClipMetaData(string name, List<float> frameDurations)
        {
            Name = name;
            FrameDurations = frameDurations;
        }
    }
}