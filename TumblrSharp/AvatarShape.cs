namespace DontPanic.TumblrSharp
{
	using System.Runtime.Serialization;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	/// <summary>
	/// Options for what shape a user's avatar is 
	/// intended to display as
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AvatarShape
	{
		/// <summary>
		/// Square avatar
		/// </summary>
		[EnumMember(Value = "square")]
		Square = 0,
		/// <summary>
		/// Circular avatar
		/// </summary
		[EnumMember(Value = "circle")]
		Circle = 1,
		/// <summary>
		/// Empty avatar shape
		/// </summary>
		[EnumMember(Value = "")]
		None = 2
	}
}