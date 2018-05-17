using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    public class Exif
    {
        [JsonProperty("camera")]
        public string Camera { get; set; }

        [JsonProperty("iso")]
        public int ISO { get; set; }

        [JsonProperty("aperture")]
        public string Aperture { get; set; }

        [JsonProperty("exposure")]
        public string Exposure { get; set; }

        [JsonProperty("focallength")]
        public string FocalLength { get; set; }
    }
}