using Newtonsoft.Json;

namespace DontPanic.TumblrSharp
{
	internal class TumblrResponseStatus
	{
		public TumblrResponseStatus()
			: this(0, null)
		{ }

		public TumblrResponseStatus(int code, string message)
		{
			Code = code;
			Message = message;
		}

		[JsonProperty(PropertyName = "status")]
		public int Code { get; set; }

		[JsonProperty(PropertyName = "msg")]
		public string Message { get; set; }
	}
}
