using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Represents an audio post.
	/// </summary>
    public class AudioPost : BasePost
    {
		/// <summary>
		/// Gets or sets the user-supplied caption.
		/// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }

		/// <summary>
		/// Gets or sets the url to the audio file.
		/// </summary>
        [JsonProperty("audio_url")]
        public string AudioUrl { get; set; }

		/// <summary>
		/// Gets or sets the HTML for embedding the audio player.
		/// </summary>
        [JsonProperty("player")]
        public string Player { get; set; }

		/// <summary>
		/// Gets or sets the number of times the audio post has been played.
		/// </summary>
        [JsonProperty("plays")]
        public int PlaysCount { get; set; }

		/// <summary>
		/// Gets or sets the location of the audio file's ID3 album art image.
		/// </summary>
        [JsonProperty("album_art")]
        public string AlbumArt { get; set; }

		/// <summary>
		/// Gets or sets the audio file's ID3 artist value
		/// </summary>
        [JsonProperty("artist")]
        public string Artist { get; set; }

		/// <summary>
		/// Gets or sets the audio file's ID3 album value.
		/// </summary>
        [JsonProperty("album")]
        public string Album { get; set; }

		/// <summary>
		/// Gets or sets the audio file's ID3 title value.
		/// </summary>
        [JsonProperty("track_name")]
        public string TrackName { get; set; }

		/// <summary>
		/// Gets or sets the audio file's ID3 track value.
		/// </summary>
        [JsonProperty("track_number")]
        public int TrackNumber { get; set; }

		/// <summary>
		/// Gets or sets the audio file's ID3 year value.
		/// </summary>
        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
