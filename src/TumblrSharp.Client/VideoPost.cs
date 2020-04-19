using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Represents a video post.
	/// </summary>
    public class VideoPost : BasePost
    {
		/// <summary>
		/// The user-supplied caption.
		/// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }

		/// <summary>
		/// Url to the video file (may be null if the video can only be embedded, i.e.: YouTube).
		/// </summary>
        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }

		/// <summary>
		/// Url to the thumbnail image for the video.
		/// </summary>
        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

		/// <summary>
		/// The thumbnail image width.
		/// </summary>
        [JsonProperty("thumbnail_width")]
        public int ThumbnailWidth { get; set; }

		/// <summary>
		/// The thumbnail image height.
		/// </summary>
        [JsonProperty("thumbnail_height")]
        public int ThumbnailHeight { get; set; }

		/// <summary>
		/// An array of <see cref="Player"/> instances.
		/// </summary>
        [JsonProperty("player")]
        public Player[] Player { get; set; }

		/// <summary>
		/// The video duration in seconds, or 0 if the duration is not specified.
		/// </summary>
		[JsonProperty("duration")]
		public double Duration { get; set; }

		/// <summary>
		/// Signals whether the video is HTML5 capable or not.
		/// </summary>
		[JsonProperty("html5_capable")]
		public bool IsHtml5Capable { get; set; }
    }

	/// <summary>
	/// Represents an embeddable player for a video in a <see cref="VideoPost"/>.
	/// </summary>
    public class Player
    {
		/// <summary>
		/// The width of the player.
		/// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

		/// <summary>
		/// The HTML code for embedding the video.
		/// </summary>
        [JsonProperty("embed_code")]
        public string EmbedCode { get; set; }
    }
}
