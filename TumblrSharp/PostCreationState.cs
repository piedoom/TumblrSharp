
namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Defines the creation state of a post.
	/// </summary>
	public enum PostCreationState
	{
		/// <summary>
		/// The post will be created as published.
		/// </summary>
		Published = 0,

		/// <summary>
		/// The post will be created as draft.
		/// </summary>
		Draft = 1,

		/// <summary>
		/// The post will be created queued.
		/// </summary>
		Queue = 2,

		/// <summary>
		/// The post will be created as private.
		/// </summary>
		Private = 3,

		/// <summary>
		/// The post is an unpublished submission
		/// </summary>
		Submission = 4



	}
}
