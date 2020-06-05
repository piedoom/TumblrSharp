using Newtonsoft.Json;

namespace DontPanic.TumblrSharp
{
	internal class TumblrRawResponse<TResponse>
	{
		[JsonProperty(PropertyName = "meta")]
		public TumblrResponseStatus Status { get; set; }

		[JsonProperty(PropertyName = "response")]
		public TResponse Response { get; set; }
	}
}
