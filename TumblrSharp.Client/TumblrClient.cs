using DontPanic.TumblrSharp.OAuth;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DontPanic.TumblrSharp.Client
{
	/// <summary>
	/// Encapsulates the Tumblr API.
	/// </summary>
	public class TumblrClient : TumblrClientBase
	{
		private bool disposed;
		private readonly string apiKey;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="TumblrClient"/> class.
		/// </summary>
        /// <param name="hashProvider">
        /// A <see cref="IHmacSha1HashProvider"/> implementation used to generate a
        /// HMAC-SHA1 hash for OAuth purposes.
        /// </param>
		/// <param name="consumerKey">
		/// The consumer key.
		/// </param>
		/// <param name="consumerSecret">
		/// The consumer secret.
		/// </param>
		/// <param name="oAuthToken">
		/// An optional access token for the API. If no access token is provided, only the methods
		/// that do not require OAuth can be invoked successfully.
		/// </param>
		/// <remarks>
		///  You can get a consumer key and a consumer secret by registering an application with Tumblr:<br/>
		/// <br/>
		/// http://www.tumblr.com/oauth/apps
		/// </remarks>
        public TumblrClient(IHmacSha1HashProvider hashProvider, string consumerKey, string consumerSecret, Token oAuthToken = null)
			: base(hashProvider, consumerKey, consumerSecret, oAuthToken)
		{
			this.apiKey = consumerKey; 
		}

		#region Public Methods

		#region Blog Methods

		#region GetBlogInfoAsync

		/// <summary>
		/// Asynchronously retrieves general information about the blog, such as the title, number of posts, and other high-level data.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#blog-info.
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="BlogInfo"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogName"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		/// </exception>
		public Task<BlogInfo> GetBlogInfoAsync(string blogName)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("api_key", apiKey);

			return CallApiMethodAsync<BlogInfoResponse, BlogInfo>(
				new BlogMethod(blogName, "info", OAuthToken, HttpMethod.Get, parameters),
				r => r.Blog,
				CancellationToken.None);
		}

		#endregion

		#region GetPostsAsync

		/// <summary>
		/// Asynchronously retrieves published posts from a blog.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#posts
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog.
		/// </param>
		/// <param name="startIndex">
		/// The offset at which to start retrieving the posts. Use 0 to start retrieving from the latest post.
		/// </param>
		/// <param name="count">
		/// The number of posts to retrieve. Must be between 1 and 20.
		/// </param>
		/// <param name="type">
		/// The <see cref="PostType"/> to retrieve.
		/// </param>
		/// <param name="includeReblogInfo">
		/// Whether or not to include reblog info with the posts.
		/// </param>
		/// <param name="includeNotesInfo">
		/// Whether or not to include notes info with the posts.
		/// </param>
		/// <param name="filter">
		/// A <see cref="PostFilter"/> to apply.
		/// </param>
		/// <param name="tag">
		/// A tag to filter by.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="Posts"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogName"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="startIndex"/> is less than 0.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="count"/> is less than 1 or greater than 20.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public Task<Posts> GetPostsAsync(string blogName, long startIndex = 0, int count = 20, PostType type = PostType.All, bool includeReblogInfo = false, bool includeNotesInfo = false, PostFilter filter = PostFilter.Html, string tag = null)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be greater or equal to zero.");

			if (count < 1 || count > 20)
				throw new ArgumentOutOfRangeException("count", "count must be between 1 and 20.");

			string methodName = null;
			switch (type)
			{
				case PostType.Text: methodName = "posts/text"; break;
				case PostType.Quote: methodName = "posts/quote"; break;
				case PostType.Link: methodName = "posts/link"; break;
				case PostType.Answer: methodName = "posts/answer"; break;
				case PostType.Video: methodName = "posts/video"; break;
				case PostType.Audio: methodName = "posts/audio"; break;
				case PostType.Photo: methodName = "posts/photo"; break;
				case PostType.Chat: methodName = "posts/chat"; break;
				case PostType.All:
				default: methodName = "posts"; break;
			}

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("api_key", apiKey);
			parameters.Add("offset", startIndex, 0);
			parameters.Add("limit", count, 0);
			parameters.Add("reblog_info", includeReblogInfo, false);
			parameters.Add("notes_info", includeNotesInfo, false);
			parameters.Add("filter", filter.ToString().ToLowerInvariant(), "html");
			parameters.Add("tag", tag);

			return CallApiMethodAsync<Posts>(
				new BlogMethod(blogName, methodName, null, HttpMethod.Get, parameters),
				CancellationToken.None);
		}

		#endregion

		#region GetPostAsync

		/// <summary>
		/// Asynchronously retrieves a specific post by id.
		/// </summary>
		/// <param name="id">
		/// The id of the post to retrieve.
		/// </param>
		/// <param name="includeReblogInfo">
		/// Whether or not to include reblog info with the posts.
		/// </param>
		/// <param name="includeNotesInfo">
		/// Whether or not to include notes info with the posts.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="BasePost"/> instance representing the desired post. Otherwise <see cref="Task.Exception"/> will carry a 
		/// <see cref="TumblrException"/> if the post with the specified id cannot be found.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///	<paramref name="id"/> is less than 0.
		/// </exception>
		public Task<BasePost> GetPostAsync(long id, bool includeReblogInfo = false, bool includeNotesInfo = false)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (id < 0)
				throw new ArgumentOutOfRangeException("id", "id must be greater or equal to zero.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("api_key", apiKey);
			parameters.Add("id", id, 0);
			parameters.Add("reblog_info", includeReblogInfo, false);
			parameters.Add("notes_info", includeNotesInfo, false);

			return CallApiMethodAsync<Posts, BasePost>(
				new BlogMethod("dummy", "posts", null, HttpMethod.Get, parameters),
				p => p.Result.FirstOrDefault(),
				CancellationToken.None);
		}

		#endregion

		#region GetBlogLikesAsync

		/// <summary>
		/// Asynchronously retrieves the publicly exposed likes from a blog.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#blog-likes
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog.
		/// </param>
		/// <param name="startIndex">
		/// The offset at which to start retrieving the likes. Use 0 to start retrieving from the latest like.
		/// </param>
		/// <param name="count">
		/// The number of likes to retrieve. Must be between 1 and 20.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="Likes"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogName"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="startIndex"/> is less than 0.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="count"/> is less than 1 or greater than 20.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public Task<Likes> GetBlogLikesAsync(string blogName, int startIndex = 0, int count = 20)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be greater or equal to zero.");

			if (count < 1 || count > 20)
				throw new ArgumentOutOfRangeException("count", "count must be between 1 and 20.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("api_key", apiKey);
			parameters.Add("offset", startIndex, 0);
			parameters.Add("limit", count, 0);

			return CallApiMethodAsync<Likes>(
				new BlogMethod(blogName, "likes", null, HttpMethod.Get, parameters),
				CancellationToken.None);
		}

		#endregion

		#region GetFollowersAsync

		/// <summary>
		/// Asynchronously retrieves a blog's followers.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#blog-followers
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog.
		/// </param>
		/// <param name="startIndex">
		/// The offset at which to start retrieving the followers. Use 0 to start retrieving from the latest follower.
		/// </param>
		/// <param name="count">
		/// The number of followers to retrieve. Must be between 1 and 20.
		/// </param>
		/// <returns>
		///  A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="Followers"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// A <see cref="Followers"/> instance.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogName"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="startIndex"/> is less than 0.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="count"/> is less than 1 or greater than 20.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public Task<Followers> GetFollowersAsync(string blogName, int startIndex = 0, int count = 20)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be greater or equal to zero.");

			if (count < 1 || count > 20)
				throw new ArgumentOutOfRangeException("count", "count must be between 1 and 20.");

			if (OAuthToken == null)
				throw new InvalidOperationException("GetFollowersAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("offset", startIndex, 0);
			parameters.Add("limit", count, 0);

			return CallApiMethodAsync<Followers>(
				new BlogMethod(blogName, "followers", OAuthToken, HttpMethod.Get, parameters),
				CancellationToken.None);
		}

		#endregion

		#region CreatePostAsync

		/// <summary>
		/// Asynchronously creates a new post.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#posting
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog where to post to (must be one of the current user's blogs).
		/// </param>
		/// <param name="postData">
		/// The data that represents the type of post to create. See <see cref="PostData"/> for how
		/// to create various post types.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="PostCreationInfo"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="blogName"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="postData"/> is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task<PostCreationInfo> CreatePostAsync(string blogName, PostData postData)
		{
			return CreatePostAsync(blogName, postData, CancellationToken.None);
		}

		/// <summary>
		/// Asynchronously creates a new post.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#posting
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog where to post to (must be one of the current user's blogs).
		/// </param>
		/// <param name="postData">
		/// The data that represents the type of post to create. See <see cref="PostData"/> for how
		/// to create various post types.
		/// </param>
		/// <param name="cancellationToken">
		/// A <see cref="CancellationToken"/> that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="PostCreationInfo"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="blogName"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="postData"/> is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task<PostCreationInfo> CreatePostAsync(string blogName, PostData postData, CancellationToken cancellationToken)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (postData == null)
				throw new ArgumentNullException("postData");

			if (OAuthToken == null)
				throw new InvalidOperationException("CreatePostAsync method requires an OAuth token to be specified.");

			return CallApiMethodAsync<PostCreationInfo>(
				new BlogMethod(blogName, "post", OAuthToken, HttpMethod.Post, postData.ToMethodParameterSet()),
				cancellationToken);
		}

		#endregion

		#region EditPostAsync

		/// <summary>
		/// Asynchronously edits an existing post.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#editing
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog where the post to edit is (must be one of the current user's blogs).
		/// </param>
		/// <param name="postId">
		/// The identifier of the post to edit.
		/// </param>
		/// <param name="postData">
		/// The data that represents the updated information for the post. See <see cref="PostData"/> for how
		/// to create various post types.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="PostCreationInfo"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="blogName"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="postData"/> is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="blogName"/> is empty.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="postId"/> is less than 0.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task<PostCreationInfo> EditPostAsync(string blogName, long postId, PostData postData)
		{
			return EditPostAsync(blogName, postId, postData, CancellationToken.None);
		}

		/// <summary>
		/// Asynchronously edits an existing post.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#editing
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog where the post to edit is (must be one of the current user's blogs).
		/// </param>
		/// <param name="postId">
		/// The identifier of the post to edit.
		/// </param>
		/// <param name="postData">
		/// The data that represents the updated information for the post. See <see cref="PostData"/> for how
		/// to create various post types.
		/// </param>
		/// <param name="cancellationToken">
		/// A <see cref="CancellationToken"/> that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="PostCreationInfo"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="blogName"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="postData"/> is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="blogName"/> is empty.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="postId"/> is less than 0.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task<PostCreationInfo> EditPostAsync(string blogName, long postId, PostData postData, CancellationToken cancellationToken)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (postId < 0)
				throw new ArgumentOutOfRangeException("postId", "Post ID must be greater or equal to zero.");

			if (postData == null)
				throw new ArgumentNullException("postData");

			if (OAuthToken == null)
				throw new InvalidOperationException("EditPostAsync method requires an OAuth token to be specified.");

			var parameters = postData.ToMethodParameterSet();
			parameters.Add("id", postId);

			return CallApiMethodAsync<PostCreationInfo>(
				new BlogMethod(blogName, "post/edit", OAuthToken, HttpMethod.Post, parameters),
				CancellationToken.None);
		}

		#endregion

		#region ReblogAsync

		/// <summary>
		/// Asynchronously reblogs a post.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#reblogging
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog where to reblog the psot (must be one of the current user's blogs).
		/// </param>
		/// <param name="postId">
		/// The identifier of the post to reblog.
		/// </param>
		/// <param name="reblogKey">
		/// The post reblog key.
		/// </param>
		/// <param name="comment">
		/// An optional comment to add to the reblog.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="PostCreationInfo"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="blogName"/> is <b>null</b>.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="reblogKey"/> is <b>null</b>.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="ArgumentException">
		/// /// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="blogName"/> is empty.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="reblogKey"/> is empty.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task<PostCreationInfo> ReblogAsync(string blogName, long postId, string reblogKey, string comment = null)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (postId <= 0)
				throw new ArgumentException("Post ID must be greater than 0.", "postId");

			if (reblogKey == null)
				throw new ArgumentNullException("reblogKey");

			if (reblogKey.Length == 0)
				throw new ArgumentException("reblogKey cannot be empty.", "reblogKey");

			if (OAuthToken == null)
				throw new InvalidOperationException("ReblogAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("id", postId);
			parameters.Add("reblog_key", reblogKey);
			parameters.Add("comment", comment, null);

			return CallApiMethodAsync<PostCreationInfo>(
				new UserMethod("post/reblog", OAuthToken, HttpMethod.Post, parameters),
				CancellationToken.None);
		}

		#endregion

		#region GetQueuedPostsAsync

		/// <summary>
		/// Asynchronously returns posts in the current user's queue.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#blog-queue
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog for which to retrieve queued posts.
		/// </param>
		/// <param name="startIndex">
		/// The offset at which to start retrieving the posts. Use 0 to start retrieving from the latest post.
		/// </param>
		/// <param name="count">
		/// The number of posts to retrieve. Must be between 1 and 20.
		/// </param>
		/// <param name="filter">
		/// A <see cref="PostFilter"/> to apply.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry an array of posts. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogName"/> is <b>null</b>.
		///	</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		///	</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="startIndex"/> is less than 0.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="count"/> is less than 1 or greater than 20.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task<BasePost[]> GetQueuedPostsAsync(string blogName, long startIndex = 0, int count = 20, PostFilter filter = PostFilter.Html)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be greater or equal to zero.");

			if (count < 1 || count > 20)
				throw new ArgumentOutOfRangeException("count", "count must be between 1 and 20.");

			if (OAuthToken == null)
				throw new InvalidOperationException("GetQueuedPostsAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("offset", startIndex, 0);
			parameters.Add("limit", count, 0);
			parameters.Add("filter", filter.ToString().ToLowerInvariant(), "html");

			return CallApiMethodAsync<PostCollection, BasePost[]>(
				new BlogMethod(blogName, "posts/queue",OAuthToken, HttpMethod.Get, parameters),
				r => r.Posts,
				CancellationToken.None);
		}

		#endregion

		#region GetDraftPostsAsync

		/// <summary>
		/// Asynchronously returns draft posts.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#blog-drafts
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog for which to retrieve drafted posts. 
		/// </param>
		/// <param name="sinceId">
		/// Return posts that have appeared after the specified ID. Use this parameter to page through 
		/// the results: first get a set of posts, and then get posts since the last ID of the previous set. 
		/// </param>
		/// <param name="filter">
		/// A <see cref="PostFilter"/> to apply.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry an array of posts. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogName"/> is <b>null</b>.
		///	</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		///	</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="sinceId"/> is less than 0.
		///	</exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task<BasePost[]> GetDraftPostsAsync(string blogName, long sinceId = 0, PostFilter filter = PostFilter.Html)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (sinceId < 0)
				throw new ArgumentOutOfRangeException("sinceId", "sinceId must be greater or equal to zero.");

			if (OAuthToken == null)
				throw new InvalidOperationException("GetDraftPostsAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("since_id", sinceId, 0);
			parameters.Add("filter", filter.ToString().ToLowerInvariant(), "html");

			return CallApiMethodAsync<PostCollection, BasePost[]>(
				new BlogMethod(blogName, "posts/draft", OAuthToken, HttpMethod.Get, parameters),
				r => r.Posts,
				CancellationToken.None);
		}

		#endregion

		#region GetSubmissionPostsAsync

		/// <summary>
		/// Asynchronously retrieves submission posts.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#blog-submissions
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog for which to retrieve submission posts. 
		/// </param>
		/// <param name="startIndex">
		/// The post number to start at. Pass 0 to start from the first post.
		/// </param>
		/// <param name="filter">
		/// A <see cref="PostFilter"/> to apply.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry an array of posts. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogName"/> is <b>null</b>.
		///	</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		///	</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="startIndex"/> is less than 0.
		///	</exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task<BasePost[]> GetSubmissionPostsAsync(string blogName, long startIndex = 0, PostFilter filter = PostFilter.Html)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be greater or equal to zero.");

			if (OAuthToken == null)
				throw new InvalidOperationException("GetSubmissionPostsAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("offset", startIndex);
			parameters.Add("filter", filter.ToString().ToLowerInvariant(), "html");

			return CallApiMethodAsync<PostCollection, BasePost[]>(
				new BlogMethod(blogName, "posts/submission", OAuthToken, HttpMethod.Get, parameters),
				r => r.Posts,
				CancellationToken.None);
		}

		#endregion

		#region DeletePostAsync

		/// <summary>
		/// Asynchronously deletes a post.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#deleting-posts
		/// </remarks>
		/// <param name="blogName">
		/// The name of the blog to which the post to delete belongs.
		/// </param>
		/// <param name="postId">
		/// The identifier of the post to delete.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task fails, <see cref="Task.Exception"/> 
		/// will carry a <see cref="TumblrException"/> representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogName"/> is <b>null</b>.
		///	</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogName"/> is empty.
		///	</exception>
		///	<exception cref="ArgumentOutOfRangeException">
		///	<paramref name="postId"/> is less than 0.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task DeletePostAsync(string blogName, long postId)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogName == null)
				throw new ArgumentNullException("blogName");

			if (blogName.Length == 0)
				throw new ArgumentException("Blog name cannot be empty.", "blogName");

			if (postId < 0)
				throw new ArgumentOutOfRangeException("postId", "Post ID must be greater or equal to zero.");

			if (OAuthToken == null)
				throw new InvalidOperationException("DeletePostAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("id", postId);

			return CallApiMethodNoResultAsync(
				new BlogMethod(blogName, "post/delete", OAuthToken, HttpMethod.Post, parameters),
				CancellationToken.None);
		}

		#endregion

		#endregion

		#region User Methods

		#region GetUserInfoAsync

		/// <summary>
		/// Asynchronously retrieves the user's account information that matches the OAuth credentials submitted with the request.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#user-methods
		/// </remarks>
		/// <returns>
		/// A <see cref="Task"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="UserInfo"/> instance. Otherwise <see cref="Task.Exception"/> will carry the <see cref="TumblrException"/>
		/// generated during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task<UserInfo> GetUserInfoAsync()
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (OAuthToken == null)
				throw new InvalidOperationException("GetUserInfoAsync method requires an OAuth token to be specified.");

			return CallApiMethodAsync<UserInfoResponse, UserInfo>(
				new UserMethod("info", OAuthToken, HttpMethod.Get),
				r => r.User,
				CancellationToken.None);
		}

		#endregion

		#region GetFollowingAsync

		/// <summary>
		/// Asynchronously retrieves the blog that the current user is following.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#m-ug-following
		/// </remarks>
		/// <param name="startIndex">
		/// The offset at which to start retrieving the followed blogs. Use 0 to start retrieving from the latest followed blog.
		/// </param>
		/// <param name="count">
		/// The number of following blogs to retrieve. Must be between 1 and 20.
		/// </param>
		/// <returns>
		/// A <see cref="Task"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="Following"/> instance. Otherwise <see cref="Task.Exception"/> will carry the <see cref="TumblrException"/>
		/// generated during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="startIndex"/> is less than 0.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="count"/> is less than 1 or greater than 20.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public Task<Following> GetFollowingAsync(long startIndex = 0, int count = 20)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be greater or equal to zero.");

			if (count <= 0 || count > 20)
				throw new ArgumentOutOfRangeException("count", "count must be between 1 and 20.");

			if (OAuthToken == null)
				throw new InvalidOperationException("GetFollowingAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("offset", startIndex, 0);
			parameters.Add("limit", count, 0);

			return CallApiMethodAsync<Following>(
				new UserMethod("following", OAuthToken, HttpMethod.Get, parameters),
				CancellationToken.None);
		}

		#endregion

		#region GetLikesAsync

		/// <summary>
		/// Asynchronously retrieves the current user's likes.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#m-ug-likes
		/// </remarks>
		/// <param name="startIndex">
		/// The offset at which to start retrieving the likes. Use 0 to start retrieving from the latest like.
		/// </param>
		/// <param name="count">
		/// The number of likes to retrieve. Must be between 1 and 20.
		/// </param>
		/// <returns>
		/// A <see cref="Task"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="Likes"/> instance. Otherwise <see cref="Task.Exception"/> will carry the <see cref="TumblrException"/>
		/// generated during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="startIndex"/> is less than 0.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="count"/> is less than 1 or greater than 20.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public Task<Likes> GetLikesAsync(long startIndex = 0, int count = 20)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be greater or equal to zero.");

			if (count <= 0 || count > 20)
				throw new ArgumentOutOfRangeException("count", "count must be between 1 and 20.");

			if (OAuthToken == null)
				throw new InvalidOperationException("GetLikesAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("offset", startIndex, 0);
			parameters.Add("limit", count, 0);

			return CallApiMethodAsync<Likes>(
				new UserMethod("likes", OAuthToken, HttpMethod.Get, parameters),
				CancellationToken.None);
		}

		#endregion

		#region LikeAsync

		/// <summary>
		/// Asynchronously likes a post.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#m-up-like
		/// </remarks>
		/// <param name="postId">
		/// The identifier of the post to like.
		/// </param>
		/// <param name="reblogKey">
		/// The reblog key for the post.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task fails, <see cref="Task.Exception"/> 
		/// will carry a <see cref="TumblrException"/>
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="reblogKey"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="reblogKey"/> is empty.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="postId"/> is less than 0.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task LikeAsync(long postId, string reblogKey)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (postId <= 0)
				throw new ArgumentOutOfRangeException("Post ID must be greater than 0.", "postId");

			if (reblogKey == null)
				throw new ArgumentNullException("reblogKey");

			if (reblogKey.Length == 0)
				throw new ArgumentException("reblogKey cannot be empty.", "reblogKey");

			if (OAuthToken == null)
				throw new InvalidOperationException("LikeAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("id", postId);
			parameters.Add("reblog_key", reblogKey);

			return CallApiMethodNoResultAsync(
				new UserMethod("like", OAuthToken, HttpMethod.Post, parameters),
				CancellationToken.None);
		}

		#endregion

		#region UnlikeAsync

		/// <summary>
		/// Asynchronously unlikes a post.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#m-up-unlike
		/// </remarks>
		/// <param name="postId">
		/// The identifier of the post to like.
		/// </param>
		/// <param name="reblogKey">
		/// The reblog key for the post.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task fails, <see cref="Task.Exception"/> 
		/// will carry a <see cref="TumblrException"/>
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="reblogKey"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="reblogKey"/> is empty.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="postId"/> is less than 0.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task UnlikeAsync(long postId, string reblogKey)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (postId <= 0)
				throw new ArgumentException("Post ID must be greater than 0.", "postId");

			if (reblogKey == null)
				throw new ArgumentNullException("reblogKey");

			if (reblogKey.Length == 0)
				throw new ArgumentException("reblogKey cannot be empty.", "reblogKey");

			if (OAuthToken == null)
				throw new InvalidOperationException("UnlikeAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("id", postId);
			parameters.Add("reblog_key", reblogKey);

			return CallApiMethodNoResultAsync(
				new UserMethod("unlike", OAuthToken, HttpMethod.Post, parameters),
				CancellationToken.None);
		}

		#endregion

		#region FollowAsync

		/// <summary>
		/// Asynchronously follows a blog.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#m-up-follow
		/// </remarks>
		/// <param name="blogUrl">
		/// The url of the blog to follow.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task fails, <see cref="Task.Exception"/> 
		/// will carry a <see cref="TumblrException"/>
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogUrl"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogUrl"/> is empty.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task FollowAsync(string blogUrl)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogUrl == null)
				throw new ArgumentNullException("blogUrl");

			if (blogUrl.Length == 0)
				throw new ArgumentException("Blog url cannot be empty.", "blogUrl");

			if (OAuthToken == null)
				throw new InvalidOperationException("FollowAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("url", blogUrl);

			return CallApiMethodNoResultAsync(
				new UserMethod("follow", OAuthToken, HttpMethod.Get, parameters),
				CancellationToken.None);
		}

		#endregion

		#region UnfollowAsync

		/// <summary>
		/// Asynchronously unfollows a blog.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#m-up-unfollow
		/// </remarks>
		/// <param name="blogUrl">
		/// The url of the blog to unfollow.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task fails, <see cref="Task.Exception"/> 
		/// will carry a <see cref="TumblrException"/>
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="blogUrl"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="blogUrl"/> is empty.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		public Task UnfollowAsync(string blogUrl)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (blogUrl == null)
				throw new ArgumentNullException("blogUrl");

			if (blogUrl.Length == 0)
				throw new ArgumentException("Blog url cannot be empty.", "blogUrl");

			if (OAuthToken == null)
				throw new InvalidOperationException("UnfollowAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("url", blogUrl);

			return CallApiMethodNoResultAsync(
				new UserMethod("unfollow", OAuthToken, HttpMethod.Get, parameters),
				CancellationToken.None);
		}

		#endregion

		#region GetTaggedPostsAsync

		/// <summary>
		/// Asynchronously retrieves posts that have been tagged with a specific <paramref name="tag"/>.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#m-up-tagged
		/// </remarks>
		/// <param name="tag">
		/// The tag on the posts to retrieve.
		/// </param>
		/// <param name="before">
		/// The timestamp of when to retrieve posts before. 
		/// </param>
		/// <param name="count">
		/// The number of posts to retrieve.
		/// </param>
		/// <param name="filter">
		/// A <see cref="PostFilter"/>.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry an array of posts. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="tag"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="tag"/> is empty.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="count"/> is less than 1 or greater than 20.
		/// </exception>
		public Task<BasePost[]> GetTaggedPostsAsync(string tag, DateTime? before = null, int count = 20, PostFilter filter = PostFilter.Html)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (tag == null)
				throw new ArgumentNullException("tag");

			if (tag.Length == 0)
				throw new ArgumentException("Tag cannot be empty.", "tag");

			if (count < 1 || count > 20)
				throw new ArgumentOutOfRangeException("count", "count must be between 1 and 20.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("api_key", apiKey);
			parameters.Add("tag", tag);
			parameters.Add("before", before.HasValue ? DateTimeHelper.ToTimestamp(before.Value).ToString() : null, null);
			parameters.Add("limit", count, 0);
			parameters.Add("filter", filter.ToString().ToLowerInvariant(), "html");

			return CallApiMethodAsync<BasePost[]>(
				new ApiMethod("https://api.tumblr.com/v2/tagged", OAuthToken, HttpMethod.Get, parameters),
				CancellationToken.None,
				new JsonConverter[] { new PostArrayConverter() });
		}

		#endregion

		#region GetDashboardPostsAsync

		/// <summary>
		/// Asynchronously retrieves posts from the current user's dashboard.
		/// </summary>
		/// See:  http://www.tumblr.com/docs/en/api/v2#m-ug-dashboard
		/// <param name="sinceId">
		///  Return posts that have appeared after the specified ID. Use this parameter to page through the results: first get a set 
		///  of posts, and then get posts since the last ID of the previous set.  
		/// </param>
		/// <param name="startIndex">
		/// The post number to start at.
		/// </param>
		/// <param name="count">
		/// The number of posts to return.
		/// </param>
		/// <param name="type">
		/// The <see cref="PostType"/> to return.
		/// </param>
		/// <param name="includeReblogInfo">
		/// Whether or not the response should include reblog info.
		/// </param>
		/// <param name="includeNotesInfo">
		/// Whether or not the response should include notes info.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry an array of posts. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="sinceId"/> is less than 0.
		///		</description>
		///	</item>
		/// <item>
		///		<description>
		///			<paramref name="startIndex"/> is less than 0.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="count"/> is less than 1 or greater than 20.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public Task<BasePost[]> GetDashboardPostsAsync(long sinceId = 0, long startIndex = 0, int count = 20, PostType type = PostType.All, bool includeReblogInfo = false, bool includeNotesInfo = false)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (sinceId < 0)
				throw new ArgumentOutOfRangeException("sinceId", "sinceId must be greater or equal to zero.");

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be greater or equal to zero.");

			if (count < 1 || count > 20)
				throw new ArgumentOutOfRangeException("count", "count must be between 1 and 20.");

			if (OAuthToken == null)
				throw new InvalidOperationException("GetDashboardPostsAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("type", type.ToString().ToLowerInvariant(), "all");
			parameters.Add("since_id", sinceId, 0);
			parameters.Add("offset", startIndex, 0);
			parameters.Add("limit", count, 0);
			parameters.Add("reblog_info", includeReblogInfo, false);
			parameters.Add("notes_info", includeNotesInfo, false);

			return CallApiMethodAsync<PostCollection, BasePost[]>(
				new UserMethod("dashboard", OAuthToken, HttpMethod.Get, parameters),
				r => r.Posts,
				CancellationToken.None);
		}

		#endregion

		#region GetUserLikesAsync

		/// <summary>
		/// Asynchronously retrieves the current user's likes.
		/// </summary>
		/// <remarks>
		/// See: http://www.tumblr.com/docs/en/api/v2#m-ug-likes
		/// </remarks>
		/// <param name="startIndex">
		/// The offset at which to start retrieving the likes. Use 0 to start retrieving from the latest like.
		/// </param>
		/// <param name="count">
		/// The number of likes to retrieve. Must be between 1 and 20.
		/// </param>
		/// <returns>
		/// A <see cref="Task{T}"/> that can be used to track the operation. If the task succeeds, the <see cref="Task{T}.Result"/> will
		/// carry a <see cref="Likes"/> instance. Otherwise <see cref="Task.Exception"/> will carry a <see cref="TumblrException"/>
		/// representing the error occurred during the call.
		/// </returns>
		/// <exception cref="ObjectDisposedException">
		/// The object has been disposed.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This <see cref="TumblrClient"/> instance does not have an OAuth token specified.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <list type="bullet">
		/// <item>
		///		<description>
		///			<paramref name="startIndex"/> is less than 0.
		///		</description>
		///	</item>
		///	<item>
		///		<description>
		///			<paramref name="count"/> is less than 1 or greater than 20.
		///		</description>
		///	</item>
		/// </list>
		/// </exception>
		public Task<Likes> GetUserLikesAsync(int startIndex = 0, int count = 20)
		{
			if (disposed)
				throw new ObjectDisposedException("TumblrClient");

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be greater or equal to zero.");

			if (count < 1 || count > 20)
				throw new ArgumentOutOfRangeException("count", "count must be between 1 and 20.");

			if (OAuthToken == null)
				throw new InvalidOperationException("GetBlogLikesAsync method requires an OAuth token to be specified.");

			MethodParameterSet parameters = new MethodParameterSet();
			parameters.Add("offset", startIndex, 0);
			parameters.Add("limit", count, 0);

			return CallApiMethodAsync<Likes>(
				new UserMethod("likes", OAuthToken, HttpMethod.Get, parameters),
				CancellationToken.None);
		}

		#endregion

		#endregion

        #endregion

		#region IDisposable Implementation

		/// <summary>
		/// Disposes of the object.
		/// </summary>
		/// <param name="disposing">
		/// <b>true</b> if managed resources have to be disposed; otherwise <b>false</b>.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			disposed = true;
			base.Dispose();
		}

		#endregion
	}
}
