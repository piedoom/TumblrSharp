using System;
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
        /// Converts the current instance to a <see cref="MethodParameterSet"/>/
        /// </summary>
        /// <returns>
        /// A <see cref="MethodParameterSet"/>.
        /// </returns>
		public MethodParameterSet ToMethodParameterSet()
		{
			var result = new MethodParameterSet(parameters);

			if (State != PostCreationState.Published)
				result.Add("state", State.ToString().ToLowerInvariant());

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the text post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="body"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="body"/> is empty.
		/// </exception>
		public static PostData CreateText(string body)
		{
			return CreateText(body, null, null, PostCreationState.Published);
		}

		/// <summary>
		/// Creates the <see cref="PostData"/> for a text post.
		/// </summary>
		/// <param name="body">
		/// The body of the text post.
		/// </param>
		/// <param name="title">
		/// The title of the text post.
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
		public static PostData CreateText(string body, string title)
		{
			return CreateText(body, title, null, PostCreationState.Published);
		}

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="body"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="body"/> is empty.
		/// </exception>
		public static PostData CreateText(string body, string title, IEnumerable<string> tags)
		{
			return CreateText(body, title, tags, PostCreationState.Published);
		}

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
		public static PostData CreateText(string body, string title, IEnumerable<string> tags, PostCreationState state)
		{
			if (body == null)
				throw new ArgumentNullException("body");

			if (body.Length == 0)
				throw new ArgumentException("Body cannot be empty.", "body");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "text");
			postData.parameters.Add("body", body);
			postData.parameters.Add("title", title, null);

			return postData;
		}

		#endregion 

		#region CreatePhoto

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
		/// <param name="caption">
		/// The caption for the photo post.
		/// </param>
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="photo"/> is <b>null</b>.
		/// </exception>
		public static PostData CreatePhoto(BinaryFile photo, string caption)
		{
			if (photo == null)
				throw new ArgumentNullException("photo");

			var photos = new BinaryFile[] { photo };
			return CreatePhoto(photos, caption, null, null, PostCreationState.Published);
		}

		/// <summary>
		/// Creates the <see cref="PostData"/> for a photo post.
		/// </summary>
		/// <param name="photos">
		/// A list of photos to upload, defined as <see cref="BinaryFile"/> instances.
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
		public static PostData CreatePhoto(IEnumerable<BinaryFile> photos)
		{
			return CreatePhoto(photos, null, null, null, PostCreationState.Published);
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="photos"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="photos"/> is empty.
		/// </exception>
		public static PostData CreatePhoto(IEnumerable<BinaryFile> photos, string caption)
		{
			return CreatePhoto(photos, caption, null, null, PostCreationState.Published);
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="photos"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="photos"/> is empty.
		/// </exception>
		public static PostData CreatePhoto(IEnumerable<BinaryFile> photos, string caption, string clickThroughUrl)
		{
			return CreatePhoto(photos, caption, clickThroughUrl, null, PostCreationState.Published);
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="photos"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="photos"/> is empty.
		/// </exception>
		public static PostData CreatePhoto(IEnumerable<BinaryFile> photos, string caption, string clickThroughUrl, IEnumerable<string> tags)
		{
			return CreatePhoto(photos, caption, clickThroughUrl, tags, PostCreationState.Published);
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
		public static PostData CreatePhoto(IEnumerable<BinaryFile> photos, string caption, string clickThroughUrl, IEnumerable<string> tags, PostCreationState state)
		{
			if (photos == null)
				throw new ArgumentNullException("photos");

			if (photos.FirstOrDefault() == null)
				throw new ArgumentException("There must be at least one photo to post.", "photos");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "photo");

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

			postData.parameters.Add("caption", caption, null);
			postData.parameters.Add("link", clickThroughUrl, null);

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="quote"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="quote"/> is empty.
		/// </exception>
		public static PostData CreateQuote(string quote)
		{
			return CreateQuote(quote, null, null, PostCreationState.Published);
		}

		/// <summary>
		/// Creates the <see cref="PostData"/> for a quote post.
		/// </summary>
		/// <param name="quote">
		/// The quote.
		/// </param>
		/// <param name="source">
		/// The quote's source.
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
		public static PostData CreateQuote(string quote, string source)
		{
			return CreateQuote(quote, source, null, PostCreationState.Published);
		}

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="quote"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="quote"/> is empty.
		/// </exception>
		public static PostData CreateQuote(string quote, string source, IEnumerable<string> tags)
		{
			return CreateQuote(quote, source, tags, PostCreationState.Published);
		}

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
		public static PostData CreateQuote(string quote, string source, IEnumerable<string> tags, PostCreationState state)
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="url"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="url"/> is empty.
		/// </exception>
		public static PostData CreateLink(string url)
		{
			return CreateLink(url, null, null, null, PostCreationState.Published);
		}

		/// <summary>
		/// Creates the <see cref="PostData"/> for a link post.
		/// </summary>
		/// <param name="url">
		/// The url for the link.
		/// </param>
		/// <param name="title">
		/// The display text for the link.
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
		public static PostData CreateLink(string url, string title)
		{
			return CreateLink(url, title, null, null, PostCreationState.Published);
		}

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="url"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="url"/> is empty.
		/// </exception>
		public static PostData CreateLink(string url, string title, string description)
		{
			return CreateLink(url, title, description, null, PostCreationState.Published);
		}

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="url"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="url"/> is empty.
		/// </exception>
		public static PostData CreateLink(string url, string title, string description, IEnumerable<string> tags)
		{
			return CreateLink(url, title, description, tags, PostCreationState.Published);
		}

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="url"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="url"/> is empty.
		/// </exception>
		public static PostData CreateLink(string url, string title, string description, IEnumerable<string> tags, PostCreationState state)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			if (url.Length == 0)
				throw new ArgumentException("Url cannot be empty.", "url");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "link");
			postData.parameters.Add("url", url);
			postData.parameters.Add("title", title, null);
			postData.parameters.Add("description", description, null);

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="conversation"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="conversation"/> is empty.
		/// </exception>
		public static PostData CreateChat(string conversation)
		{
			return CreateChat(conversation, null, null, PostCreationState.Published);
		}

		/// <summary>
		/// Creates the <see cref="PostData"/> for a chat post.
		/// </summary>
		/// <param name="conversation">
		/// The chat conversation.
		/// </param>
		/// <param name="title">
		/// The title of the chat.
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
		public static PostData CreateChat(string conversation, string title)
		{
			return CreateChat(conversation, title, null, PostCreationState.Published);
		}

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="conversation"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="conversation"/> is empty.
		/// </exception>
		public static PostData CreateChat(string conversation, string title, IEnumerable<string> tags)
		{
			return CreateChat(conversation, title, tags, PostCreationState.Published);
		}

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
		public static PostData CreateChat(string conversation, string title, IEnumerable<string> tags, PostCreationState state)
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="audioFile"/> is <b>null</b>.
		/// </exception>
		public static PostData CreateAudio(BinaryFile audioFile)
		{
			return CreateAudio(audioFile, null, null, PostCreationState.Published);
		}

		/// <summary>
		/// Creates the <see cref="PostData"/> for an audio post.
		/// </summary>
		/// <param name="audioFile">
		/// The audio file to upload, defined as a <see cref="BinaryFile"/> instance.
		/// </param>
		/// <param name="caption">
		/// The caption for the audio post.
		/// </param>
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="audioFile"/> is <b>null</b>.
		/// </exception>
		public static PostData CreateAudio(BinaryFile audioFile, string caption)
		{
			return CreateAudio(audioFile, caption, null, PostCreationState.Published);
		}

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="audioFile"/> is <b>null</b>.
		/// </exception>
		public static PostData CreateAudio(BinaryFile audioFile, string caption, IEnumerable<string> tags)
		{
			return CreateAudio(audioFile, caption, tags, PostCreationState.Published);
		}

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
		public static PostData CreateAudio(BinaryFile audioFile, string caption, IEnumerable<string> tags, PostCreationState state)
		{
			if (audioFile == null)
				throw new ArgumentNullException("audioFile");

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="url"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="url"/> is empty.
		/// </exception>
		public static PostData CreateAudio(string url)
		{
			return CreateAudio(url, null, null, PostCreationState.Published);
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="url"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="url"/> is empty.
		/// </exception>
		public static PostData CreateAudio(string url, string caption)
		{
			return CreateAudio(url, caption, null, PostCreationState.Published);
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="url"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="url"/> is empty.
		/// </exception>
		public static PostData CreateAudio(string url, string caption, IEnumerable<string> tags)
		{
			return CreateAudio(url, caption, tags, PostCreationState.Published);
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
		public static PostData CreateAudio(string url, string caption, IEnumerable<string> tags, PostCreationState state)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			if (url.Length == 0)
				throw new ArgumentException("Url cannot be empty.", "url");

			var postData = new PostData(state, tags);
			postData.parameters.Add("type", "audio");
			postData.parameters.Add("url", url);
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="videoFile"/> is <b>null</b>.
		/// </exception>
		public static PostData CreateVideo(BinaryFile videoFile)
		{
			return CreateVideo(videoFile, null, null, PostCreationState.Published);
		}

		/// <summary>
		/// Creates the <see cref="PostData"/> for a video post.
		/// </summary>
		/// <param name="videoFile">
		/// The video file to upload, defined as a <see cref="BinaryFile"/> instance.
		/// </param>
		/// <param name="caption">
		/// The caption for the video post.
		/// </param>
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="videoFile"/> is <b>null</b>.
		/// </exception>
		public static PostData CreateVideo(BinaryFile videoFile, string caption)
		{
			return CreateVideo(videoFile, caption, null, PostCreationState.Published);
		}

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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="videoFile"/> is <b>null</b>.
		/// </exception>
		public static PostData CreateVideo(BinaryFile videoFile, string caption, IEnumerable<string> tags)
		{
			return CreateVideo(videoFile, caption, tags, PostCreationState.Published);
		}

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
		public static PostData CreateVideo(BinaryFile videoFile, string caption, IEnumerable<string> tags, PostCreationState state)
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="embedCode"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="embedCode"/> is empty.
		/// </exception>
		public static PostData CreateVideo(string embedCode)
		{
			return CreateVideo(embedCode, null, null, PostCreationState.Published);
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="embedCode"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="embedCode"/> is empty.
		/// </exception>
		public static PostData CreateVideo(string embedCode, string caption)
		{
			return CreateVideo(embedCode, caption, null, PostCreationState.Published);
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
		/// <returns>
		/// A <see cref="PostData"/> instance representing the post.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="embedCode"/> is <b>null</b>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="embedCode"/> is empty.
		/// </exception>
		public static PostData CreateVideo(string embedCode, string caption, IEnumerable<string> tags)
		{
			return CreateVideo(embedCode, caption, tags, PostCreationState.Published);
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
		public static PostData CreateVideo(string embedCode, string caption, IEnumerable<string> tags, PostCreationState state)
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
