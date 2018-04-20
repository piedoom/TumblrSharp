using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Represents a photo post.
	/// </summary>
    public class PhotoPost : BasePost
    {
		/// <summary>
		/// The user-supplied caption.
		/// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }

		/// <summary>
		/// An array of <see cref="Photo"/> instances.
		/// </summary>
        [JsonProperty("photos")]
        public Photo[] PhotoSet { get; set; }

        /// <summary>
        /// Gets the main (or only, for photo posts with only one picture) photo for the post.
        /// </summary>
        public Photo Photo { get { return (PhotoSet != null && PhotoSet.Length > 0) ? PhotoSet[0] : null; } }
	}

	/// <summary>
	/// Represents a photo in a <see cref="PhotoPost"/>.
	/// </summary>
    public class Photo
    {
		/// <summary>
		/// User supplied caption for the individual photo (Photosets only).
		/// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }

		/// <summary>
		/// An array of <see cref="PhotoInfo"/> instances for alternate (thumbnail)
		/// versions of the photo.
		/// </summary>
		[JsonProperty("alt_sizes")]
		public PhotoInfo[] AlternateSizes { get; set; }

		/// <summary>
		/// A <see cref="PhotoInfo"/> instance representing the details of the full-size photo.
		/// </summary>
        [JsonProperty("original_size")]
        public PhotoInfo OriginalSize { get; set; }
	}

	/// <summary>
	/// Represents information about a <see cref="Photo"/>.
	/// </summary>
    public class PhotoInfo
    {
		/// <summary>
		/// The photo width, in pixels.
		/// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

		/// <summary>
		/// The photo height, in pixels.
		/// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

		/// <summary>
		/// The url of the photo.
		/// </summary>
		[JsonProperty("url")]
		public string ImageUrl { get; set; }
	}
}
