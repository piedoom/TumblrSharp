using Newtonsoft.Json;

namespace DontPanic.TumblrSharp
{
	internal class TumblrErrorResponse
	{
		[JsonProperty(PropertyName = "meta")]
		public TumblrResponseStatus Status { get; set; }

		[JsonConverter(typeof(TumblrErrorsConverter))]
		[JsonProperty(PropertyName = "response")]
		public TumblrErrors Response { get; set; }
	}
}
