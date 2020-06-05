
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace DontPanic.TumblrSharp
{
    /// <summary>
    /// Defines the creation state of a post.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PostCreationState
	{
        /// <summary>
        /// The post will be created as published.
        /// </summary>
        [EnumMember(Value = "published")]
        Published = 0,

        /// <summary>
        /// The post will be created as draft.
        /// </summary>
        [EnumMember(Value = "draft")]
        Draft = 1,

        /// <summary>
        /// The post will be created queued.
        /// </summary>
        [EnumMember(Value = "queued")]
        Queue = 2,

        /// <summary>
        /// The post will be created as private.
        /// </summary>
        [EnumMember(Value = "private")]
        Private = 3,

        /// <summary>
        /// The post is an unpublished submission
        /// </summary>
        [EnumMember(Value = "submission")]
        Submission = 4
	}
}
