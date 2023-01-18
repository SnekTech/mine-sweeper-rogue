using System.Collections.Generic;
using Newtonsoft.Json;

namespace SnekTech.Editor.Animation
{
    public class AsepriteJsonData
    {
        [JsonProperty("frames")]
        public List<FrameData> Frames { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }

    public class FrameData
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }
        
        [JsonProperty("duration")]
        public int Duration { get; set; }
    }

    public class Meta
    {
        [JsonProperty("app")]
        public string App { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("frameTags")]
        public List<FrameTag> Tags { get; set; }
    }

    public class FrameTag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("from")]
        public int FromIndex { get; set; }

        [JsonProperty("to")]
        public int ToIndex { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        public int Length => ToIndex - FromIndex + 1;
    }
}