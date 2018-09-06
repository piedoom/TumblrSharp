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
        /// Empty avatar shape
        /// </summary>
        [EnumMember(Value = "")]
        None = 0,
        /// <summary>
		/// Square avatar
		/// </summary>
		[EnumMember(Value = "square")]
		Square = 1,
		/// <summary>
		/// Circular avatar
		/// </summary>
		[EnumMember(Value = "circle")]
		Circle = 2,
		/// <summary>
		/// rounded avatar
		/// </summary>
		[EnumMember(Value = "rounded")]
        Rounded = 3,
        /// <summary>
		/// circle avatar
		/// </summary>
		[EnumMember(Value = "avatar-circle")]
        AvatarCircle = 4,
        /// <summary>
		/// square avatar
		/// </summary>
		[EnumMember(Value = "avatar-square")]
        AvatarSquare = 5,
        /// <summary>
        /// rounded avatar
        /// </summary>
        [EnumMember(Value = "avatar-rounded")]
        AvatarRounded = 6
    }
}