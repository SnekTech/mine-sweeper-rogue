using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SnekTech.Editor.Animation
{
    public static class AsepriteJsonHandler
    {
        #region Aseprite JSON field name constants

        private const string MetaField = "meta";
        private const string LayersField = "layers";
        private const string LayerNameField = "name";
        private const string FramesField = "frames";
        private const string FileNameField = "filename";
        private const string FrameDurationField = "duration";

        #endregion

        public static List<AsepriteClipMetaData> ExtractClipMetaData(string jsonText)
        {
            var frameNameToFrameDurations = new Dictionary<string, List<float>>();
            var asepriteJson = JObject.Parse(jsonText);
            
            // extract layer metadata
            var meta = (JObject) asepriteJson[MetaField];
            var layers = meta![LayersField] as JArray;
            foreach (var layer in layers!)
            {
                var layerObject = (JObject) layer;
                frameNameToFrameDurations[((string) layerObject[LayerNameField])!] = new List<float>();
            }
            
            // extract frame metadata
            var frames = (JArray) asepriteJson[FramesField]!;
            foreach (var frame in frames)
            {
                var frameObject = (JObject) frame;
                float duration = (float)frameObject[FrameDurationField];
                string fileName = (string) frameObject[FileNameField];

                int frameNameStart = fileName!.IndexOf('(') + 1;
                int frameNameEnd = fileName!.IndexOf(')');
                string frameName = fileName.Substring(frameNameStart, frameNameEnd - frameNameStart);
                frameNameToFrameDurations[frameName].Add(duration);
            }

            // create the clip metadata list and return
            var clipMetaDataList = new List<AsepriteClipMetaData>();
            foreach ((string frameName, var durations) in frameNameToFrameDurations)
            {
                var clipMetaData = new AsepriteClipMetaData(frameName, durations);
                clipMetaDataList.Add(clipMetaData);
            }

            return clipMetaDataList;
        }
    }
}