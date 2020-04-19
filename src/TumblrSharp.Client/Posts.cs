using Newtonsoft.Json;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Contains information about a blog's posts.
	/// </summary>
	public class Posts
    {
		/// <summary>
		/// A <see cref="BlogInfo"/> instance representing information about
		/// the blog for which the posts are being retrieved.
		/// </summary>
        [JsonProperty(PropertyName = "blog")]
        public BlogInfo Blog { get; set; }

		/// <summary>
		/// An array of <see cref="BasePost"/> instances, containing the 
		/// blog's posts.
		/// </summary>
		[JsonConverter(typeof(PostArrayConverter))]
		[JsonProperty(PropertyName = "posts")]
		public BasePost[] Result { get; set; }
    }
}
