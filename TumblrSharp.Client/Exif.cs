using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// Exif
    /// </summary>
    public class Exif
    {
        /// <summary>
        /// camera
        /// </summary>
        [JsonProperty("camera")]
        public string Camera { get; set; }

        /// <summary>
        /// iso
        /// </summary>
        [JsonProperty("iso")]
        public int ISO { get; set; }

        /// <summary>
        /// aperture
        /// </summary>
        [JsonProperty("aperture")]
        public string Aperture { get; set; }

        /// <summary>
        /// exposure
        /// </summary>
        [JsonProperty("exposure")]
        public string Exposure { get; set; }

        /// <summary>
        /// focallength
        /// </summary>
        [JsonProperty("focallength")]
        public string FocalLength { get; set; }
    }
}