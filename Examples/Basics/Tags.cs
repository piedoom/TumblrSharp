using DontPanic.TumblrSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Basics
{
    [Serializable]
    public class TagsIllegalCharacterException : Exception
    {
        public TagsIllegalCharacterException() { }

        public TagsIllegalCharacterException(string message) : base(message) { }

        public TagsIllegalCharacterException(string message, Exception inner) : base(message, inner) { }

        protected TagsIllegalCharacterException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class Tags
    {
        public Tags(IEnumerable<string> tags = null, bool caseSensitive = false, TumblrClient tumblrClient = null)
        {
            this.tags = new List<string>();

            postTags = new HashSet<string>();

            if (caseSensitive) CaseSensitive = true;

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    Add(tag);
                }
            }

            if (tumblrClient != null)
            {
                this.tumblrClient = tumblrClient;
            }

            LoadTagsFromPosts();
        }

        #region private fields

        private List<string> tags;

        private TumblrClient tumblrClient = null;

        private HashSet<string> postTags;

        #endregion

        #region private methode

        private async void LoadTagsFromPosts()
        {
            if (tumblrClient != null)
            {
                var userInfo = await tumblrClient.GetUserInfoAsync();

                var blogs = userInfo.Blogs;

                foreach (var blog in blogs)
                {
                    var blogInfo = await tumblrClient.GetBlogInfoAsync(blog.Name);

                    for (int i = 0; i < blogInfo.PostsCount; i = i + 20)
                    {
                        var posts = await tumblrClient.GetPostsAsync(blog.Name, i);

                        Parallel.ForEach(posts.Result, (post) =>
                        {
                            Parallel.For(0, post.Tags.Length - 1, (k) =>
                            {
                                string tag = post.Tags[k];

                                postTags.Add(tag);
                            }
                            );
                        }
                        );
                    }
                }
            }
        }
        
        #endregion

        #region property

        public bool CaseSensitive { get; set; } = false;

        #endregion

        #region methode

        public List<string> GetLookupList()
        {
            List<string> result = new List<string>();

            if (tags != null) result.AddRange(tags);

            if (postTags != null) result.AddRange(postTags);

            result = (new HashSet<string>(result)).ToList();

            return result;
        }

        public void Add(string tag)
        {
            if (tag.IndexOf('#') > -1)
            {
                throw new TagsIllegalCharacterException("Illegal Character #");
            }

            if (CaseSensitive == false)
            {
                string findTag = tags.Find(x => (x == tag) || (x.ToLower() == tag.ToLower()));

                if (findTag == null)
                {
                    tags.Add(tag);
                }
            }
            else
            {
                string findTag = tags.Find(x => x == tag);

                if (findTag == null)
                {
                    tags.Add(tag);
                }
            }
        }

        public void Clear() => tags.Clear();

        public List<string> ToList() => tags;

        public string[] ToArray() => tags.ToArray();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var tag in tags)
            {
                sb.Append("#");
                sb.Append(tag);
                sb.Append(" ");
            }

            return sb.ToString();
        }

        #endregion

    }
}
