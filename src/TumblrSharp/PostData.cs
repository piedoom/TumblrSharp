﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Represents a post to be submitted to a Tumblr blog.
	/// </summary>
	/// <remarks>
	/// This class cannot be instantiated directly. Use one of the static factory methods to
	/// create an instance of this class.
	/// </remarks>
	public class PostData
	{
		private readonly MethodParameterSet parameters;

		internal PostData(PostCreationState state, IEnumerable<string> tags)
		{
			parameters = new MethodParameterSet();

			State = state;
			Tags = (tags != null && tags.FirstOrDefault() != null) ? new List<string>(tags) : new List<string>();
			Format = PostFormat.Html;
		}

		/// <summary>
		/// Gets or sets the <see cref="PostCreationState"/> of the post.
		/// </summary>
		public PostCreationState State { get; set; }

		/// <summary>
		/// Gets the tags associated with the post.
		/// </summary>
		public List<string> Tags { get; private set; }

		/// <summary>
		/// Gets or sets the autotweet (if enabled) for this post: set to <b>off</b> 
		/// for no tweet, or enter text to override the default tweet.
		/// </summary>
		public string Tweet { get; set; }

		/// <summary>
		/// Gets or sets the post date.
		/// </summary>
		public DateTimeOffset? Date { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="PostFormat"/>.
		/// </summary>
		public PostFormat Format { get; set; }
	
		/// <summary>
		/// Gets or sets the post slug.
		/// </summary>
		public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the DateTime of publish this, if <see cref="PostCreationState"/> is <see cref="PostCreationState.Queue"/>
        /// </summary>
        public DateTime? Publish_On { get; set; }

        /// <summary>
        /// Converts the current instance to a <see cref="MethodParameterSet"/>/
        /// </summary>
        /// <returns>
        /// A <see cref="MethodParameterSet"/>.
        /// </returns>
		public MethodParameterSet ToMethodParameterSet()
		{
			var result = new MethodParameterSet(parameters);				

			if (Tags != null)
				result.Add("tags", String.Join(",", Tags.ToArray()));

			if (!String.IsNullOrEmpty(Tweet))
				result.Add("tweet", Tweet);

			if (Date != null)
				result.Add("date", Date.Value.ToUniversalTime().ToString("R"));

			if (Format != PostFormat.Html)
				result.Add("format", Format.ToString().ToLowerInvariant());

			if (!String.IsNullOrEmpty(Slug))
				result.Add("slug", Slug);

            result.Add("state", State.ToString().ToLowerInvariant());

            if (State == PostCreationState.Queue)
            {
                if (Publish_On != null)
                {
                    result.Add("publish_on", DateTimeHelper.ToTimestamp(Publish_On.Value));
                }
            }

            return result;
		}

        #region Static Methods

        #region CreateText

		/// <summary>
		/// Creates the <see cref="PostData"/> for a text post.
		/// </summary>
		/// <param name="body">
		/// The body of the text post.
		/// </param>
		/// <param name="title">
		/// The title of the text post.
		/// </param>
		/// <param name="tags">
		/// The tags to associate with the post.
		/// </param>
		/// <param name="state">
		/// The <see cref="PostCreationState"/> of the post.
		/// </param>
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="body"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="body"/> is empty.
		/// </exception>
		public static PostData CreateText(
            string body = null, 
            string title = null, 
            IEnumerable<string> tags = null, 
            PostCreationState state = PostCreationState.Published)
		{
			if (body == null && title == null)
				throw new ArgumentException("Must have at least body or title.");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "text");
			postData.parameters.Add("body", body, null);
			postData.parameters.Add("title", title, null);

			return postData;
		}

        #endregion

        #region CreatePhoto

        /// <summary>
        /// Creates the <see cref="PostData"/> for a photo post with a source url for photo.
        /// </summary>
        /// <param name="sourceUrl">The photo source URL</param>
        /// <param name="caption">The user-supplied caption, HTML allowed</param>
        /// <param name="clickThroughUrl">The "click-through URL" for the photo</param>
        /// <param name="tags">list of tags for this post</param>
        /// <param name="state">The state of the post. Specify one of the following: published, draft, queue, private</param>
        /// <returns>A <see cref="PostData"/> instance representing the post.</returns>
        /// <exception cref="ArgumentNullException">the url for photo source must be set and not null</exception>
        /// <exception cref="ArgumentException">the url for photo source must be set</exception>
        public static PostData CreatePhoto(
            string sourceUrl,
            string caption = null,
            string clickThroughUrl = null,
            IEnumerable<string> tags = null,
            PostCreationState state = PostCreationState.Published)
        {
            if (sourceUrl == null)
                throw new ArgumentNullException(nameof(sourceUrl));

            if (sourceUrl.Length == 0)
                throw new ArgumentException("The photo source URL be not empty.", nameof(sourceUrl));

            var postData = new PostData(state, tags);
            postData.parameters.Add("type", "photo");
			postData.parameters.Add("source", sourceUrl);
            postData.parameters.Add("caption", caption, null);
            postData.parameters.Add("link", clickThroughUrl, null);

            return postData;
        }

        /// <summary>
        /// Creates the <see cref="PostData"/> for a photo post.
        /// </summary>
        /// <param name="photo">
        /// A photo to upload, defined as a <see cref="BinaryFile"/> instance.
        /// </param>
        /// <returns>
        /// A <see cref="PostData"/> instance representing the post.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="photo"/> is <b>null</b>.
        /// </exception>
        public static PostData CreatePhoto(BinaryFile photo)
		{
			if (photo == null)
				throw new ArgumentNullException("photo");

			var photos = new BinaryFile[] { photo };
			return CreatePhoto(photos, null, null, null, PostCreationState.Published);
		}

        /// <summary>
        /// Creates the <see cref="PostData"/> for a photo post.
        /// </summary>
        /// <param name="photo">
        /// A photo to upload, defined as a <see cref="BinaryFile"/> instance.
        /// </param>
        /// <param name="state">
        /// The <see cref="PostCreationState"/> of this photo post.
        /// </param>
        /// <param name="caption">
        /// The optional string caption for this photo post.
        /// </param>
        /// <param name="clickThroughUrl">
        /// The photo(s) click trough url.
        /// </param>
        /// <param name="tags">
        /// The optional array of string used for tags.
        /// </param>
        /// <returns>
        /// A <see cref="PostData"/> instance representing the post.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="photo"/> is <b>null</b>.
        /// </exception>
        public static PostData CreatePhoto(
            BinaryFile photo, 
            string caption = null,
            string clickThroughUrl = null,
            IEnumerable<string> tags = null,
            PostCreationState state = PostCreationState.Published)
		{
			if (photo == null)
				throw new ArgumentNullException("photo");

			var photos = new BinaryFile[] { photo };

			return CreatePhoto(photos, caption, clickThroughUrl, tags, state);
		}

		/// <summary>
		/// Creates the <see cref="PostData"/> for a photo post.
		/// </summary>
		/// <param name="photos">
		/// A list of photos to upload, defined as <see cref="BinaryFile"/> instances.
		/// </param>
		/// <param name="caption">
		/// The caption for the photo post.
		/// </param>
		/// <param name="clickThroughUrl">
		/// The photo(s) click trough url.
		/// </param>
		/// <param name="tags">
		/// The tags to associate with the post.
		/// </param>
		/// <param name="state">
		/// The <see cref="PostCreationState"/> of the post.
		/// </param>
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="photos"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="photos"/> is empty.
		/// </exception>
		public static PostData CreatePhoto(
            IEnumerable<BinaryFile> photos, 
            string caption = null, 
            string clickThroughUrl = null, 
            IEnumerable<string> tags = null, 
            PostCreationState state = PostCreationState.Published)
		{
			if (photos == null)
				throw new ArgumentNullException("photos");

			if (photos.FirstOrDefault() == null)
				throw new ArgumentException("There must be at least one photo to post.", "photos");

            foreach (var photo in photos)
            {
                if (photo.Data.LongLength >= 1048576)
                    throw new ArgumentException("The size of photo must smaller 10 MiB", "photos");
            }

            var postData = new PostData(state, tags);
			postData.parameters.Add("type", "photo");
            postData.parameters.Add("caption", caption, null);
            postData.parameters.Add("link", clickThroughUrl, null);

            if (photos.Count() == 1)
			{
				var photo = photos.First();
				postData.parameters.Add(new BinaryMethodParameter("data", photo.Data, photo.FileName, photo.MimeType));
			}
			else
			{
				int i = 0;
				foreach (var photo in photos)
					postData.parameters.Add(new BinaryMethodParameter(String.Format("data[{0}]", i++), photo.Data, photo.FileName, photo.MimeType));
			}

			return postData;
		}

		#endregion

		#region CreateQuote

		/// <summary>
		/// Creates the <see cref="PostData"/> for a quote post.
		/// </summary>
		/// <param name="quote">
		/// The quote.
		/// </param>
		/// <param name="source">
		/// The quote's source.
		/// </param>
		/// <param name="tags">
		/// The tags to associate with the post.
		/// </param>
		/// <param name="state">
		/// The <see cref="PostCreationState"/> of the post.
		/// </param>
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="quote"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="quote"/> is empty.
		/// </exception>
		public static PostData CreateQuote(
            string quote, 
            string source, 
            IEnumerable<string> tags = null, 
            PostCreationState state = PostCreationState.Published)
		{
			if (quote == null)
				throw new ArgumentNullException("quote");

			if (quote.Length == 0)
				throw new ArgumentException("Quote cannot be empty.", "quote");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "quote");
			postData.parameters.Add("quote", quote);
			postData.parameters.Add("source", source, null);

			return postData;
		}

        #endregion

        #region CreateLink

        /// <summary>
        /// Creates the <see cref="PostData"/> for a link post.
        /// </summary>
        /// <param name="url">
        /// The url for the link.
        /// </param>
        /// <param name="title">
        /// The display text for the link.
        /// </param>
        /// <param name="description">
        /// The link's description.
        /// </param>
        /// <param name="tags">
        /// The tags to associate with the post.
        /// </param>
        /// <param name="state">
        /// The <see cref="PostCreationState"/> of the post.
        /// </param>
        /// <param name="thumbnail">
        /// The url of an image to use as a thumbnail for the post
        /// </param>
        /// <param name="excerpt">
        /// An excerpt from the page the link points to, HTML entities should be escaped
        /// </param>
        /// <param name="author">
        /// The name of the author from the page the link points to, HTML entities should be escaped
        /// </param>
        /// <returns>
        /// A <see cref="PostData"/> instance representing the post.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="url"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="url"/> is empty.
        /// </exception>
        public static PostData CreateLink(
            string url,
            string title = null,
            string description = null,
            string thumbnail = null,
            string excerpt = null,
            string author = null,
            IEnumerable<string> tags = null,
            PostCreationState state = PostCreationState.Published)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            if (url.Length == 0)
                throw new ArgumentException("Url cannot be empty.", nameof(url));

            var postData = new PostData(state, tags);
            postData.parameters.Add("type", "link");
            postData.parameters.Add("url", url);
            postData.parameters.Add("title", title, null);
            postData.parameters.Add("description", description, null);
            postData.parameters.Add("thumbnail", thumbnail, null);
            postData.parameters.Add("excerpt", excerpt, null);
            postData.parameters.Add("author", author, null);

            return postData;
        }

        #endregion

        #region CreateChat

        /// <summary>
        /// Creates the <see cref="PostData"/> for a chat post.
        /// </summary>
        /// <param name="conversation">
        /// The chat conversation.
        /// </param>
        /// <param name="title">
        /// The title of the chat.
        /// </param>
        /// <param name="tags">
        /// The tags to associate with the post.
        /// </param>
        /// <param name="state">
        /// The <see cref="PostCreationState"/> of the post.
        /// </param>
        /// <returns>
        /// A <see cref="PostData"/> instance representing the post.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="conversation"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="conversation"/> is empty.
        /// </exception>
        public static PostData CreateChat(
            string conversation, 
            string title = null, 
            IEnumerable<string> tags = null, 
            PostCreationState state = PostCreationState.Published)
		{
			if (conversation == null)
				throw new ArgumentNullException("conversation");

			if (conversation.Length == 0)
				throw new ArgumentException("Conversation cannot be empty.", "conversation");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "chat");
			postData.parameters.Add("conversation", conversation);
			postData.parameters.Add("title", title, null);

			return postData;
		}

        #endregion

        #region CreateAudio

        /// <summary>
        /// Creates the <see cref="PostData"/> for an audio post.
        /// </summary>
        /// <param name="audioFile">
        /// The audio file to upload, defined as a <see cref="BinaryFile"/> instance.
        /// </param>
        /// <param name="caption">
        /// The caption for the audio post.
        /// </param>
        /// <param name="tags">
        /// The tags to associate with the post.
        /// </param>
        /// <param name="state">
        /// The <see cref="PostCreationState"/> of the post.
        /// </param>
        /// <returns>
        /// A <see cref="PostData"/> instance representing the post.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="audioFile"/> is <b>null</b>.
        /// </exception>
        /// <remarks>
        /// The audio file can only be posted to Tumblr once, otherwise the post will be rejected with a “Bad request” response.
		/// Likewise, the size limit appears to be less than the 10 MB specified in the official API documentation.
        /// </remarks>
        public static PostData CreateAudio(
            BinaryFile audioFile, 
            string caption = null, 
            IEnumerable<string> tags = null, 
            PostCreationState state = PostCreationState.Published)
		{
			if (audioFile == null)
				throw new ArgumentNullException("audioFile");

			if (audioFile.Data.Length  >= 10_000_000)
                throw new ArgumentException("The size of photo must smaller 10 MB", "audioFile");

            var postData = new PostData(state, tags);
			postData.parameters.Add("type", "audio");
			postData.parameters.Add(new BinaryMethodParameter("data", audioFile.Data, audioFile.FileName, audioFile.MimeType));
			postData.parameters.Add("caption", caption, null);

			return postData;
		}

        /// <summary>
        /// Creates the <see cref="PostData"/> for an audio post.
        /// </summary>
        /// <param name="url">
        /// The url to the audio file to post (the url must not be on Tumblr).
        /// </param>
        /// <param name="caption">
        /// The caption for the audio post.
        /// </param>
        /// <param name="tags">
        /// The tags to associate with the post.
        /// </param>
        /// <param name="state">
        /// The <see cref="PostCreationState"/> of the post.
        /// </param>
        /// <returns>
        /// A <see cref="PostData"/> instance representing the post.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="url"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="url"/> is empty.
        /// </exception>
        /// /// <remarks>
        /// The audio file can only be posted to Tumblr once, otherwise the post will be rejected with a “Bad request” response.
        /// Likewise, the size limit appears to be less than the 10 MB specified in the official API documentation.
        /// </remarks>
        public static PostData CreateAudio(
            string url, 
            string caption = null, 
            IEnumerable<string> tags = null, 
            PostCreationState state = PostCreationState.Published)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			if (url.Length == 0)
				throw new ArgumentException("Url cannot be empty.", "url");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "audio");
			postData.parameters.Add("external_url", url);
			postData.parameters.Add("caption", caption, null);

			return postData;
		}

		#endregion

		#region CreateVideo

		/// <summary>
		/// Creates the <see cref="PostData"/> for a video post.
		/// </summary>
		/// <param name="videoFile">
		/// The video file to upload, defined as a <see cref="BinaryFile"/> instance.
		/// </param>
		/// <param name="caption">
		/// The caption for the video post.
		/// </param>
		/// <param name="tags">
		/// The tags to associate with the post.
		/// </param>
		/// <param name="state">
		/// The <see cref="PostCreationState"/> of the post.
		/// </param>
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="videoFile"/> is <b>null</b>.
		/// </exception>
		public static PostData CreateVideo(
            BinaryFile videoFile, 
            string caption = null, 
            IEnumerable<string> tags = null, 
            PostCreationState state = PostCreationState.Published)
		{
			if (videoFile == null)
				throw new ArgumentNullException("videoFile");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "video");
			postData.parameters.Add(new BinaryMethodParameter("data", videoFile.Data, videoFile.FileName, videoFile.MimeType));
			postData.parameters.Add("caption", caption, null);

			return postData;
		}

		/// <summary>
		/// Creates the <see cref="PostData"/> for a video post.
		/// </summary>
		/// <param name="embedCode">
		/// The HTML embed code for the video.
		/// </param>
		/// <param name="caption">
		/// The caption for the video post.
		/// </param>
		/// <param name="tags">
		/// The tags to associate with the post.
		/// </param>
		/// <param name="state">
		/// The <see cref="PostCreationState"/> of the post.
		/// </param>
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="embedCode"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="embedCode"/> is empty.
		/// </exception>
		public static PostData CreateVideo(
            string embedCode, 
            string caption = null, 
            IEnumerable<string> tags = null, 
            PostCreationState state = PostCreationState.Published)
		{
			if (embedCode == null)
				throw new ArgumentNullException("embedCode");

			if (embedCode.Length == 0)
				throw new ArgumentException("Embed Code cannot be empty.", "embedCode");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "video");
			postData.parameters.Add("embed", embedCode);
			postData.parameters.Add("caption", caption, null);

			return postData;
		}

		#endregion

		#endregion
	}
}
