
namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Defines the filter for a post.
	/// </summary>
	public enum PostFilter
	{
		/// <summary>
		/// No filter.
		/// </summary>
		Html = 0,

		/// <summary>
		/// Plain text, no html.
		/// </summary>
		Text = 1,

		/// <summary>
		/// As entered by the user (no post-processing); if the user writes in Markdown, the Markdown will be returned rather than html.
		/// </summary>
		Raw = 2
	}
}
